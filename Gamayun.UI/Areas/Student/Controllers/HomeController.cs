
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Student.Controllers
{
    public class HomeController : StudentController
    {
        public HomeController(GridQueryRunner queryRunner) : base(queryRunner)
        {
        }

        public ViewResult Index() => View();
    }
}
