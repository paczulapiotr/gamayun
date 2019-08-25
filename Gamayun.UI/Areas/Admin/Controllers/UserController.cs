using System;
using System.Linq;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Command.Admin;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly GamayunDbContext _dbContext;

        public UserController(
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings,
            GamayunDbContext dbContext) 
            : base(commandRunner, gridQueryRunner, settings)
        {
            _dbContext = dbContext;
        }

        public ViewResult AdminSearch()
             => View(new GridConfiguration<UserRM>
             {
                 DataUrl = GetActionUrl(nameof(AdminSearchQuery)),
             });

        [HttpPost]
        public JsonResult AdminSearchQuery([FromBody]GridFilters<UserRM> filter)
            => Json(_gridQueryRunner.Run(filter, new AdminsQueryHandler.Query()));

        public ViewResult TeacherSearch()
            => View(new GridConfiguration<UserRM>
            {
                DataUrl = GetActionUrl(nameof(TeacherSearchQuery))
            });

        [HttpPost]
        public JsonResult TeacherSearchQuery([FromBody]GridFilters<UserRM> filter)
           => Json(_gridQueryRunner.Run(filter, new TeachersQueryHandler.Query()));

        public ViewResult StudentSearch()
            => View(new GridConfiguration<UserRM>
            {
                DataUrl = GetActionUrl(nameof(StudentSearchQuery)),
            });

        [HttpPost]
        public JsonResult StudentSearchQuery([FromBody]GridFilters<UserRM> filter)
           => Json(_gridQueryRunner.Run(filter, new StudentsQueryHandler.Query()));

        public ViewResult StudentView() => View();
        public ViewResult StudentCreate() => View();
        public ActionResult StudentEdit(int id)
        {
            var vm = _dbContext.Students.Include(x => x.AppUser).Select(x =>
            new EditUserCommandHandler.StudentCommand {
                Id= x.ID,
                Username = x.AppUser.UserName,
                FirstName = x.AppUser.FirstName,
                LastName = x.AppUser.LastName,
                Email = x.AppUser.Email,
            }).FirstOrDefault(x => x.Id == id);
            if (vm == null)
            {
                return RedirectToAction(nameof(Error));
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult StudentCreate(CreateUserCommandHandler.StudentCommand command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(StudentSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();
        }

        [HttpPost]
        public ActionResult StudentEdit(EditUserCommandHandler.StudentCommand command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(StudentSearch));
            }

            ViewBag.Errors = result.Errors;
            return View(command.Id);
        }

        public ViewResult AdminView() => View();
        public ViewResult AdminCreate() => View();
        public ActionResult AdminEdit(int id)
        {
            var vm = _dbContext.Admins.Include(x => x.AppUser).Select(x =>
          new EditUserCommandHandler.StudentCommand
          {
              Id = x.ID,
              Username = x.AppUser.UserName,
              FirstName = x.AppUser.FirstName,
              LastName = x.AppUser.LastName,
              Email = x.AppUser.Email,
          }).FirstOrDefault(x => x.Id == id);
            if (vm == null)
            {
                return RedirectToAction(nameof(Error));
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult AdminCreate(CreateUserCommandHandler.AdminCommand command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AdminSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();

        }

        [HttpPost]
        public ActionResult AdminEdit(EditUserCommandHandler.AdminCommand command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AdminSearch));
            }

            ViewBag.Errors = result.Errors;
            return View(command.Id);

        }

        public ViewResult TeacherView() => View();
        public ViewResult TeacherCreate() => View();
        public ActionResult TeacherEdit(int id)
        {
            var vm = _dbContext.Teachers.Include(x => x.AppUser).Select(x =>
          new EditUserCommandHandler.StudentCommand
          {
              Id = x.ID,
              Username = x.AppUser.UserName,
              FirstName = x.AppUser.FirstName,
              LastName = x.AppUser.LastName,
              Email = x.AppUser.Email,
          }).FirstOrDefault(x => x.Id == id);
            if (vm == null)
            {
                return RedirectToAction(nameof(Error));
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult TeacherCreate(CreateUserCommandHandler.TeacherCommand command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(TeacherSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();
        }

        [HttpPost]
        public ActionResult TeacherEdit(EditUserCommandHandler.TeacherCommand command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(TeacherSearch));
            }

            ViewBag.Errors = result.Errors;
            return View(command.Id);

        }

    }
}
