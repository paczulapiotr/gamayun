using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public class Command: ICommand
        {

        }
    }
}
