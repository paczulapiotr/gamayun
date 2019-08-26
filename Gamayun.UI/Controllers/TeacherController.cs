using Gamayun.Identity;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Areas.Teacher.Controllers;
using Gamayun.UI.Models;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Controllers
{
    [GamayunArea("Teacher")]
    [Authorize(Roles = AppRoles.Teacher)]
    public abstract class TeacherController : GamayunController
    {
        protected readonly GamayunDbContext _dbContext;
        protected readonly UserManager<AppUser> _userManager;

        public TeacherController(
            GamayunDbContext dbContext,
            UserManager<AppUser> userManager,
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings) 
            : base(commandRunner, gridQueryRunner, settings)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        protected async Task<int?> GetTeacherId()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return null;

            var teacher = _dbContext.Teachers.FirstOrDefault(x => x.AppUserID == user.Id);

            return teacher?.ID;
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
                                CategoryName = "Topics",
                                Icon = Icons.Admin,
                                Options= new List<SideMenuCategoryOption>
                                {
                                    new SideMenuCategoryOption(
                                        "Search Page",
                                        this.GetActionUrl<TopicController>(nameof(TopicController.TopicSearch))),
                                     new SideMenuCategoryOption(
                                        "Create New",
                                        this.GetActionUrl<TopicController>(nameof(TopicController.TopicCreate))),
                                }
                            },
                        }
                    },
                }
            };
        }
    }
}
