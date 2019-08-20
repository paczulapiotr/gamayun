using Gamayun.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public ViewResult Index() => View();
    }
}
