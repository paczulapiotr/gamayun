using Gamayun.Identity;
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
        public AdminController(IGridQueryRunner queryRunner, ISettings settings) : base(queryRunner, settings)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.SideMenuTree = new SideMenuTree
            {
                Leaves = new List<SideMenuLeaf>()
                {
                    new SideMenuLeaf
                    {
                        HeaderName = "Main menu",
                        Categories = new List<SideMenuCategory>
                        {
                            new SideMenuCategory
                            {
                                CategoryName = "Majors",
                                Options = new List<SideMenuCategoryOption>()
                                {
                                    new SideMenuCategoryOption
                                    {
                                        OptionName = "Search",
                                        AnchorHref = "/majors/search"
                                    },
                                    new SideMenuCategoryOption
                                    {
                                        OptionName = "Create new",
                                        AnchorHref = "/majors/create"
                                    }
                                }
                            },
                            new SideMenuCategory
                            {
                                AnchorHref = "#",
                                CategoryName = "Direct link bro"
                            }
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
                                        this.GetActionUrl<UserController>(nameof(UserController.AdminSearch)))
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
                                        this.GetActionUrl<UserController>(nameof(UserController.StudentSearch)))
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
                                        this.GetActionUrl<UserController>(nameof(UserController.TeacherSearch)))
                                }
                            }

                        }
                    }
                }
            };
        }
    }
}
