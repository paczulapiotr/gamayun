using Gamayun.Identity;
using Gamayun.Infrastucture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamayun.Infrastucture.Command.Admin
{
    public class CreateUserCommandHandler 
        : ICommandHandler<CreateUserCommandHandler.AdminCommand>, 
        ICommandHandler<CreateUserCommandHandler.StudentCommand>, 
        ICommandHandler<CreateUserCommandHandler.TeacherCommand>
    {
        private readonly GamayunDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(
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
                return CreateUser(_dbContext.Teachers, command).Result;
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
                return CreateUser(_dbContext.Students, command).Result;
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
                return CreateUser(_dbContext.Admins, command).Result;
            }
            catch
            {
                return CommandResult.Failed();
            }

        }

        private async Task<CommandResult> CreateUser<TUser>(IQueryable<TUser> users, CommandTemplate command) where TUser : UserWithRole
        {
            if (command.Password.Length < 5) 
            {
                return CommandResult.Failed("Password has to have minimumt 5 letters");
            }
            if (command.Username.Length < 5) 
            {
                return CommandResult.Failed("Username has to have minimumt 5 letters");
            }
            if (command.Password != command.RepeatPassword)
            {
                return CommandResult.Failed("Passwords must be matching");
            }
            if(command.Email != command.RepeatEmail)
            {
                return CommandResult.Failed("Emails must be matching");
            }
            if (users.Any(u => u.AppUser.UserName == command.Username)) 
            {
                return CommandResult.Failed("Username is already taken");
            }
            if(users.Any(u => u.AppUser.Email == command.Email))
            {
                return CommandResult.Failed("Email is already taken");
            }

            var user = new AppUser
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.Username,
                Email = command.Email,
                EmailConfirmed = true,
            };
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, command.Password);
            var result = await _userManager.CreateAsync(user);

            
            if(!result.Succeeded)
            {
                return CommandResult.Failed();
            }
            
            switch (command)
            {
                case StudentCommand s:
                    result = await _userManager.AddToRoleAsync(user, AppRoles.Student);
                    break;
                case TeacherCommand t: 
                    result = await _userManager.AddToRoleAsync(user, AppRoles.Teacher);
                    break;
                case AdminCommand a: 
                    result = await _userManager.AddToRoleAsync(user, AppRoles.Admin);
                    break;
                default:
                    break;
            }
            
            if (!result.Succeeded)
            {
                return CommandResult.Failed();
            }

            switch (command)
            {
                case StudentCommand s:
                    _dbContext.Add(new Student { AppUser = user });
                    break;
                case TeacherCommand t:
                    _dbContext.Add(new Teacher { AppUser = user });
                    break;
                case AdminCommand a:
                    _dbContext.Add(new Entities.Admin { AppUser = user });
                    break;
                default:
                    break;
            }
            _dbContext.SaveChanges();

            return CommandResult.Success();
        }

        public abstract class CommandTemplate : ICommand
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string RepeatEmail { get; set; }
            public string Password { get; set; }
            public string RepeatPassword { get; set; }
        }

        public class AdminCommand : CommandTemplate { }
        public class StudentCommand : CommandTemplate { }
        public class TeacherCommand : CommandTemplate { }
    }
}
