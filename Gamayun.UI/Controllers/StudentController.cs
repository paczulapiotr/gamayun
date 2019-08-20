using Gamayun.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Controllers
{
    [Area("Student")]
    [Authorize(Roles=AppRoles.Student)]
    public class StudentController : Controller
    {
        public StudentController()
        {

        }
    }
}
