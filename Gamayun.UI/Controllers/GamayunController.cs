using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Query;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace Gamayun.UI.Controllers
{
    public class GamayunController : Controller
    {
        protected readonly ICommandRunner _commandRunner;
        protected readonly IGridQueryRunner _gridQueryRunner;
        protected readonly ISettings _settings;

        public GamayunController(
            ICommandRunner commandRunner,
            IGridQueryRunner gridQueryRunner, 
            ISettings settings)
        {
            _commandRunner = commandRunner;
            _gridQueryRunner = gridQueryRunner;
            _settings = settings;
        }

        protected ActionResult ErrorResult()
        {
            return RedirectToAction(nameof(Error));
        }

        public ViewResult Error() => View("_Error");

        protected string GetControllerName(Type type)
        {
            return type.Name.Replace(nameof(Controller), "");
        }
        
        protected string GetControllerName() => GetControllerName(this.GetType());
        
        protected string GetActionUrl(Type controllerType, string actionName)
        {
            var area = controllerType.GetCustomAttribute<GamayunArea>()?.AreaName;

            if (!string.IsNullOrWhiteSpace(area))
            {
                return $"{_settings.RootUrl}/{area}/{GetControllerName(controllerType)}/{actionName}";
            }
            else
            {
                return $"{_settings.RootUrl}/{GetControllerName(controllerType)}/{actionName}";
            }
        }

        protected string GetActionUrl(string actionName) => GetActionUrl(this.GetType(), actionName);
        protected string GetActionUrl<TController>(string actionName) where TController : ControllerBase
            => GetActionUrl(typeof(TController), actionName);
    }
}
