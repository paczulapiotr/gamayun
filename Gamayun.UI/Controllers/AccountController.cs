using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Register()
        {
            return View();
        } 
    }
}
