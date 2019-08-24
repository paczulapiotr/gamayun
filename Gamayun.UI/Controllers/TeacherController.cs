using Gamayun.Identity;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Models;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Gamayun.UI.Controllers
{
    [GamayunArea("Teacher")]
    [Authorize(Roles = AppRoles.Teacher)]
    public abstract class TeacherController : GamayunController
    {
        public TeacherController(IGridQueryRunner queryRunner, ISettings settings) : base(queryRunner, settings)
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
                                CategoryName = "Direct category",
                                AnchorHref="/test/href",
                                Icon="",
                            }
                        }
                    }
                }
            };
        }
    }
}
