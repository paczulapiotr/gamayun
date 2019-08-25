using System;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Command.Admin;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        public UserController(
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings) 
            : base(commandRunner, gridQueryRunner, settings)
        {
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
        public ViewResult StudentEdit() => View();
        
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
        public ViewResult StudentEdit(int a)
        {
            throw new NotImplementedException();
        }

        public ViewResult AdminView() => View();
        public ViewResult AdminCreate() => View();
        public ViewResult AdminEdit() => View();
        
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
        public ViewResult AdminEdit(int a)
        {
            throw new NotImplementedException();

        }

        public ViewResult TeacherView() => View();
        public ViewResult TeacherCreate() => View();
        public ViewResult TeacherEdit() => View();
        
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
        public ViewResult TeacherEdit(int a)
        {
            throw new NotImplementedException();

        }

    }
}
