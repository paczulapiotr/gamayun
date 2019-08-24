using Gamayun.Infrastucture.Query;
using Gamayun.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public HomeController(GridQueryRunner queryRunner) : base(queryRunner)
        {
        }

        public ViewResult Index() => View();
    }
}
