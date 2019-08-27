using Gamayun.Infrastucture.Entities;
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

            _dbContext.Sections.Add(new Section
            {
                Name = command.Name,
                State = SectionState.Created,
                TopicID = command.TopicId
            });
            _dbContext.SaveChanges();
            return CommandResult.Success();
        }

        public class Command: ICommand
        {
            public string Name { get; set; }
            public int? TopicId { get; set; }
        }
    }
}
