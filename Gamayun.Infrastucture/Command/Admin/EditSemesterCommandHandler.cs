using Gamayun.Infrastucture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamayun.Infrastucture.Command.Admin
{
    public class EditSemesterCommandHandler : ICommandHandler<EditSemesterCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public EditSemesterCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommandResult Handle(Command command)
        {
            if (string.IsNullOrWhiteSpace(command.Major) || command.Major.Length < 3)
            {
                return CommandResult.Failed("Major has to have minimum length of 3 letters");
            }
            if (command.FinishedOn == null || command.FinishedOn.Value.Date <= DateTime.Today)
            {
                return CommandResult.Failed("Finished date must be in future");
            }

            var semester = _dbContext.Semesters.FirstOrDefault(x => x.ID == command.ID);
            semester.FinishedOn = command.FinishedOn.Value;
            semester.Major = command.Major;
            _dbContext.SaveChanges();

            return CommandResult.Success();
        }

        public class Command : ICommand
        {
            public int ID { get; set; }
            public string Major { get; set; }
            public DateTime? FinishedOn { get; set; }
        }
    }
}
