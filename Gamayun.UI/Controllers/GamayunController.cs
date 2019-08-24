using Gamayun.Infrastucture.Query;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace Gamayun.UI.Controllers
{
    public class GamayunController : Controller
    {
        protected readonly IGridQueryRunner _queryRunner;
        protected readonly ISettings _settings;

        public GamayunController(
            IGridQueryRunner queryRunner, 
            ISettings settings)
        {
            _queryRunner = queryRunner;
            _settings = settings;
        }

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
