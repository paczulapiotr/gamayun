﻿
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Areas.Student.Controllers
{
    public class HomeController : StudentController
    {
        public HomeController(
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings) 
            : base(commandRunner, gridQueryRunner, settings)
        {
        }

        public ViewResult Index() => View();
    }
}
