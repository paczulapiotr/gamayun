using Gamayun.Identity;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Teacher.Controllers
{
    public class HomeController : TeacherController
    {
        public HomeController(
            GamayunDbContext dbContext, 
            UserManager<AppUser> userManager, 
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings) 
            : base(dbContext, userManager, commandRunner, gridQueryRunner, settings)
        {
        }

        public ViewResult Index() => View("_Home");
    }
}
