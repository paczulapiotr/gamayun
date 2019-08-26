using Gamayun.Infrastucture.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Gamayun.Infrastucture.Command.Admin
{
    public class CreateSemesterCommandHandler : ICommandHandler<CreateSemesterCommandHandler.Command>
    {
        private readonly GamayunDbContext _dbContext;

        public CreateSemesterCommandHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICommandResult Handle(Command command)
        {
            if(string.IsNullOrWhiteSpace(command.Major) || command.Major.Length < 3)
            {
                return CommandResult.Failed("Major has to have minimum length of 3 letters");
            }
            if(command.FinishedOn == null || command.FinishedOn.Value.Date <= DateTime.Now)
            {
                return CommandResult.Failed("Finished date must be in future");
            }

            var semester = new Semester {
                CreatedOn = DateTime.Now,
                Major = command.Major,
                FinishedOn = command.FinishedOn.Value,
            };
            _dbContext.Semesters.Add(semester);
            _dbContext.SaveChanges();

            return CommandResult.Success();
        }

        public class Command : ICommand
        {
            public string Major { get; set; }
            public DateTime? FinishedOn { get; set; }
        }
    }
}
