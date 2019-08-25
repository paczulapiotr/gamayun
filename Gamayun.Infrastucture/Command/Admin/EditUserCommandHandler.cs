using Gamayun.Identity;
using Gamayun.Infrastucture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.Infrastucture.Command.Admin
{
    public class EditUserCommandHandler
        : ICommandHandler<EditUserCommandHandler.AdminCommand>,
        ICommandHandler<EditUserCommandHandler.StudentCommand>,
        ICommandHandler<EditUserCommandHandler.TeacherCommand>
    {
        private readonly GamayunDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public EditUserCommandHandler(
            GamayunDbContext dbContext,
            UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public ICommandResult Handle(TeacherCommand command)
        {
            try
            {
                return EditUser(_dbContext.Students.Include(x=>x.AppUser), command).Result;
            }
            catch
            {
                return CommandResult.Failed();
            }
        }

        public ICommandResult Handle(StudentCommand command)
        {
            try
            {
                return EditUser(_dbContext.Students.Include(x => x.AppUser), command).Result;
            }
            catch
            {
                return CommandResult.Failed();
            }
        }

        public ICommandResult Handle(AdminCommand command)
        {
            try
            {
                return EditUser(_dbContext.Students.Include(x => x.AppUser), command).Result;
            }
            catch
            {
                return CommandResult.Failed();
            }
        }

        private async Task<CommandResult> EditUser<TUser>(IQueryable<TUser> users, CommandTemplate command) where TUser : UserWithRole
        {
            var user = users.FirstOrDefault(x => x.ID == command.Id);

            if(user == null || user.AppUser == null)
            {
                return CommandResult.Failed("Given user is invalid");
            }
            if (command.Username.Length < 5)
            {
                return CommandResult.Failed("Username has to have minimumt 5 letters");
            }
            if (users.Any(u => u.ID != command.Id && u.AppUser.UserName == command.Username))
            {
                return CommandResult.Failed("Username is already taken");
            }
            if (users.Any(u => u.ID != command.Id && u.AppUser.Email == command.Email))
            {
                return CommandResult.Failed("Email is already taken");
            }

            var appUser = user.AppUser;
            appUser.Email = command.Email;
            appUser.FirstName = command.FirstName;
            appUser.LastName= command.LastName;
            var result = await _userManager.UpdateAsync(appUser);


            if (!result.Succeeded)
            {
                return CommandResult.Failed();
            }

            return CommandResult.Success();
        }

        public abstract class CommandTemplate : ICommand
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }

        public class AdminCommand : CommandTemplate { }
        public class StudentCommand : CommandTemplate { }
        public class TeacherCommand : CommandTemplate { }
    }
}
