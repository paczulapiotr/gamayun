using Gamayun.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=AppRoles.Admin)]
    public class AdminController : Controller
    {
        public AdminController()
        {

        }
    }
}
