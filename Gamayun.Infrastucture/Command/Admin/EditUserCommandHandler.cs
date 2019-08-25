using System;

namespace Gamayun.Infrastucture.Command.Admin
{
    public class EditUserCommandHandler
        : ICommandHandler<EditUserCommandHandler.AdminCommand>,
        ICommandHandler<EditUserCommandHandler.StudentCommand>,
        ICommandHandler<EditUserCommandHandler.TeacherCommand>
    {
        public ICommandResult Handle(TeacherCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(StudentCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(AdminCommand command)
        {
            throw new NotImplementedException();
        }

        public abstract class CommandTemplate : ICommand
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class AdminCommand : CommandTemplate { }
        public class StudentCommand : CommandTemplate { }
        public class TeacherCommand : CommandTemplate { }
    }
}
