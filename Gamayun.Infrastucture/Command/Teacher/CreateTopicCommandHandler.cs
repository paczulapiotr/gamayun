using Gamayun.Infrastucture.Entities;

namespace Gamayun.Infrastucture.Command.Teacher
{
    public class CreateTopicCommandHandler : ICommandHandler<CreateTopicCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public CreateTopicCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICommandResult Handle(Command command)
        {
            try
            {
                var topic = new Topic
                {
                    TeacherID = command.TeacherID,
                    Name = command.Name,
                    Description = command.Description,
                };
                _dbContext.Topics.Add(topic);
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
            public string Name { get; set; }
            public string Description { get; set; }
            public int TeacherID { get; set; }
        }
    }
}
