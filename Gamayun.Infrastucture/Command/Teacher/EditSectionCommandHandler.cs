using Gamayun.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamayun.Infrastucture.Command.Teacher
{
    public class EditSectionCommandHandler : ICommandHandler<EditSectionCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public EditSectionCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommandResult Handle(Command command)
        {
            var section = _dbContext.Sections
                .Include(x=>x.PresenceDates).ThenInclude(y=>y.Presences)
                .Include(x=>x.StudentSections)
                .FirstOrDefault(x => x.ID == command.Id);
            if (section == null)
            {
                return CommandResult.Failed("Invalid section");
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                return CommandResult.Failed("Section name cannot be empty");
            }
            switch (section.State)
            {
                case SectionState.Created:
                    switch (command.State)
                    {
                        case SectionState.Closed:
                            return CommandResult.Failed("Invalid section state");
                        default:
                            break;
                    }
                    break;
                case SectionState.Active:
                case SectionState.Closed:
                case SectionState.Canceled:
                    switch (command.State)
                    {
                        case SectionState.Created:
                            return CommandResult.Failed("Invalid section state");
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            section.Name = command.Name;
            section.State = command.State;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var currentUsers = section.StudentSections.Select(x => x.StudentID).ToList();
                    var newUsers = command.StudentIds;
                    var usersToRemove = currentUsers.Except(newUsers);
                    var studentEntitiesToRemove = section.StudentSections.Where(x => usersToRemove.Contains(x.StudentID)).ToList();
                    foreach (var student in studentEntitiesToRemove)
                    {
                        _dbContext.Attach(student);
                        _dbContext.Entry(student).State = EntityState.Deleted;
                    }
                    _dbContext.SaveChanges();

                    foreach (var presence in section.PresenceDates)
                    {
                        var presencesToRemove = presence.Presences.Where(x => usersToRemove.Contains(x.StudentID ?? 0)).ToList();
                        _dbContext.AttachRange(presencesToRemove);
                        foreach (var pres in presencesToRemove)
                        {
                            _dbContext.Entry(pres).State = EntityState.Deleted;
                        }
                    }
                    _dbContext.SaveChanges();

                    var usersToAdd = newUsers.Except(currentUsers);

                    foreach (var student in usersToAdd)
                    {
                        _dbContext.StudentSections.Add(new StudentSection
                        {
                            SectionID = command.Id,
                            StudentID = student,
                        });
                    }
                    _dbContext.SaveChanges();
                    
                    foreach (var presence in section.PresenceDates)
                    {
                        _dbContext.Attach(presence);
                        foreach (var user in usersToAdd)
                        {
                            presence.Presences.Add(new Presence
                            {
                                StudentID = user,
                            });
                        }
                    }

                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return CommandResult.Failed();
                }
            }

            return CommandResult.Success();
        }

        public class Command : ICommand
        {
            public string Name { get; set; }
            public IEnumerable<int> StudentIds { get; set; }
            public SectionState State { get; set; }
            public int Id { get; set; }
        }
    }
}
