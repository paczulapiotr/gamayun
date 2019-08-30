using Gamayun.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamayun.Infrastucture.Command.Teacher
{
    public class UpdateSectionPresencesCommandHandler : ICommandHandler<UpdateSectionPresencesCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public UpdateSectionPresencesCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ICommandResult Handle(Command command)
        {
            var dateGrouped = command.PresenceData.GroupBy(x => x.dateId).ToList();
            var section = _dbContext.Sections
                .Include(x => x.PresenceDates).ThenInclude(y => y.Presences)
                .FirstOrDefault(x => x.ID == command.SectionId);

            if (section == null)
            {
                return CommandResult.Failed();
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var presences = section.PresenceDates.SelectMany(x => x.Presences).ToList();
                    _dbContext.AttachRange(presences);
                    foreach (var pres in presences)
                    {
                        _dbContext.Entry(pres).State = EntityState.Deleted;
                    }
                    
                    _dbContext.SaveChanges();

                    dateGrouped.ForEach(x =>
                    {
                        var presence = section.PresenceDates.FirstOrDefault(y => y.ID == x.Key);
                        _dbContext.Attach(presence);

                        if (presence == null)
                            return;

                        presence.Presences.AddRange(x.Select(z => new Presence
                        {
                            StudentID = z.studentId,
                            WasPresent = z.wasPresent,
                        }));
                    });

                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return CommandResult.Failed();
                }
            }

            return CommandResult.Success();
        }

        public class Command : ICommand
        {
            public Command(IEnumerable<(int studentId, int dateId, bool wasPresent)> presenceData, int sectionId)
            {
                PresenceData = presenceData;
                SectionId = sectionId;
            }

            public IEnumerable<(int studentId, int dateId, bool wasPresent)> PresenceData { get; set; }
            public int SectionId { get; set; }
        }
    }
}
