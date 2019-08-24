using Gamayun.Infrastucture.Query;
using Gamayun.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Teacher.Controllers
{
    public class HomeController : TeacherController
    {
        public HomeController(GridQueryRunner queryRunner) : base(queryRunner)
        {
        }

        public ViewResult Index() => View();
    }
}
