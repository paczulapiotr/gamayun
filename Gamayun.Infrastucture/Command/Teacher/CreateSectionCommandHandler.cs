using Gamayun.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gamayun.Infrastucture.Command.Teacher
{
    public class CreateSectionCommandHandler : ICommandHandler<CreateSectionCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public CreateSectionCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommandResult Handle(Command command)
        {
            if(string.IsNullOrWhiteSpace(command.Name))
            {
                return CommandResult.Failed("Section name cannot be empty");
            }
            if(command.TopicId == null)
            {
                return CommandResult.Failed("Topic is required");
            }
            if(!_dbContext.Topics.Any(x=>x.ID == command.TopicId))
            {
                return CommandResult.Failed("Given topic doesn't exist");
            }
            if (!_dbContext.Semesters
                .Where(x=>x.IsActive && x.ID == command.SemesterId)
                .Any())
            {
                return CommandResult.Failed("Given semester is invalid");
            }

            //Transaction
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var section = new Section
                    {
                        Name = command.Name,
                        State = SectionState.Created,
                        TopicID = command.TopicId,
                        SemesterID = command.SemesterId,
                    };
                    _dbContext.Sections.Add(section);
                    _dbContext.SaveChanges();

                    // Add students

                    var studentIds = new List<int>();
                    int result;
                    command.Students.Split(',').ToList().ForEach(x =>
                    {
                        if (int.TryParse(x, out result))
                        {
                            studentIds.Add(result);
                        }
                    });

                    var studentSections = _dbContext.Students
                        .Include(x => x.AppUser)
                        .Where(x => !x.AppUser.IsObsolete)
                        .Where(x => studentIds.Contains(x.ID))
                        .Select(x => new StudentSection
                        {
                            Section = section,
                            Student = x
                        }).ToList();

                    _dbContext.StudentSections.AddRange(studentSections);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {

                    transaction.Rollback();
                    return CommandResult.Failed();
                }
            }
          
            return CommandResult.Success();
        }

        public class Command: ICommand
        {
            public string Name { get; set; }
            public int? TopicId { get; set; }
            public int? SemesterId { get; set; }
            public string Students { get; set; }
        }
    }
}
