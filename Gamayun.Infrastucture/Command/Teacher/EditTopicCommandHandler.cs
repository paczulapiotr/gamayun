using System.Linq;

namespace Gamayun.Infrastucture.Command.Teacher
{
    public class EditTopicCommandHandler : ICommandHandler<EditTopicCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public EditTopicCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommandResult Handle(Command command)
        {
            try
            {
                var topic = _dbContext.Topics.FirstOrDefault(x => x.ID == command.Id);
                if(topic == null)
                {
                    return CommandResult.Failed("Given topic doesn't exist");
                }
                topic.Name = command.Name;
                topic.Description = command.Description;
                _dbContext.SaveChanges();
                return CommandResult.Success();
            }
            catch
            {
                return CommandResult.Failed();
            }
        }

        public class Command : ICommand
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
