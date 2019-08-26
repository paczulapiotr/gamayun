using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        public class Command: ICommand
        {
        }
    }
}
