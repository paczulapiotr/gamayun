using Gamayun.Identity;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Areas.Admin.Controllers;
using Gamayun.UI.Models;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Gamayun.UI.Controllers
{
    [GamayunArea("Admin")]
    [Authorize(Roles=AppRoles.Admin)]
    public abstract class AdminController : GamayunController
    {

        public AdminController(
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings) 
            : base(commandRunner, gridQueryRunner, settings)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.SideMenuTree = new SideMenuTree
            {
                Leaves = new List<SideMenuLeaf>()
                {
                    new SideMenuLeaf { 
                        HeaderName = "Domain",
                        Categories = new List<SideMenuCategory>
                        {
                            new SideMenuCategory
                            {
                                CategoryName = "Semesters",
                                Icon = Icons.Admin,
                                Options= new List<SideMenuCategoryOption>
                                {
                                    new SideMenuCategoryOption(
                                        "Search Page",
                                        this.GetActionUrl<SemesterController>(nameof(SemesterController.SemesterSearch))),
                                     new SideMenuCategoryOption(
                                        "Create New",
                                        this.GetActionUrl<SemesterController>(nameof(SemesterController.SemesterCreate))),
                                }
                            },
                        }
                    },
                    new SideMenuLeaf
                    {
                        HeaderName = "Users",
                        Categories = new List<SideMenuCategory>
                        {
                            new SideMenuCategory
                            {
                                CategoryName = "Admins",
                                Icon = Icons.Admin,
                                Options= new List<SideMenuCategoryOption>
                                {
                                    new SideMenuCategoryOption(
                                        "Search Page", 
                                        this.GetActionUrl<UserController>(nameof(UserController.AdminSearch))),
                                     new SideMenuCategoryOption(
                                        "Create New",
                                        this.GetActionUrl<UserController>(nameof(UserController.AdminCreate))),
                                }
                            },
                            new SideMenuCategory
                            {
                                CategoryName = "Students",
                                Icon = Icons.Student,
                                Options= new List<SideMenuCategoryOption>
                                {
                                    new SideMenuCategoryOption(
                                        "Search Page", 
                                        this.GetActionUrl<UserController>(nameof(UserController.StudentSearch))),
                                    new SideMenuCategoryOption(
                                        "Create New",
                                        this.GetActionUrl<UserController>(nameof(UserController.StudentCreate))),

                                }
                            },
                            new SideMenuCategory
                            {
                                CategoryName = "Teachers",
                                Icon = Icons.Teacher,
                                Options= new List<SideMenuCategoryOption>
                                {
                                    new SideMenuCategoryOption(
                                        "Search Page", 
                                        this.GetActionUrl<UserController>(nameof(UserController.TeacherSearch))),
                                     new SideMenuCategoryOption(
                                        "Create New",
                                        this.GetActionUrl<UserController>(nameof(UserController.TeacherCreate))),
                                }
                            }

                        }
                    }
                }
            };
        }
    }
}
