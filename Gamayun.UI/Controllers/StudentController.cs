using Gamayun.Identity;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Areas.Student.Controllers;
using Gamayun.UI.Models;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Gamayun.UI.Controllers
{
    [GamayunArea("Student")]
    [Authorize(Roles = AppRoles.Student)]
    public abstract class StudentController : GamayunController
    {
        public StudentController(
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
                        HeaderName = "Main menu",
                        Categories = new List<SideMenuCategory>
                        {
                             new SideMenuCategory
                            {
                                CategoryName = "Sections",
                                Icon = Icons.Admin,
                                Options= new List<SideMenuCategoryOption>
                                {
                                    new SideMenuCategoryOption(
                                        "Find Sections",
                                        this.GetActionUrl<SectionController>(nameof(SectionController.FindSections))),
                                     new SideMenuCategoryOption(
                                        "My Sections",
                                        this.GetActionUrl<SectionController>(nameof(SectionController.MySections))),
                                }
                            },
                        }
                    },
                }
            };
        }
    }
}
