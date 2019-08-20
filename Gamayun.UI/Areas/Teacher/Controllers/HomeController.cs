using Gamayun.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Teacher.Controllers
{
    public class HomeController : TeacherController
    {
        public ViewResult Index() => View();
    }
}
