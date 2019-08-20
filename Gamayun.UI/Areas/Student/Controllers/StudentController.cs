
using Gamayun.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Student.Controllers
{
    public class HomeController : StudentController
    {
        public ViewResult Index() => View();
    }
}
