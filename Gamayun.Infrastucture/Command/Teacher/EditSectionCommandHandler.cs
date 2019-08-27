using Gamayun.Infrastucture.Entities;
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
            var section = _dbContext.Sections.FirstOrDefault(x => x.ID == command.Id);
            if(section == null)
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
            _dbContext.SaveChanges();

            return CommandResult.Success();
        }

        public class Command: ICommand
        {
            public string Name { get; set; }
            public SectionState State { get; set; }
            public int Id { get; set; }
        }
    }
}
