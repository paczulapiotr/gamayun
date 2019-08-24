using Gamayun.Infrastucture.Query;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public HomeController(IGridQueryRunner queryRunner, ISettings settings) : base(queryRunner, settings)
        {
        }

        public ViewResult Index() => View();
    }
}
