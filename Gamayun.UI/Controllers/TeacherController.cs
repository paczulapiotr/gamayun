using Gamayun.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = AppRoles.Teacher)]
    public class TeacherController : Controller
    {
        public TeacherController()
        {
        }
    }
}
