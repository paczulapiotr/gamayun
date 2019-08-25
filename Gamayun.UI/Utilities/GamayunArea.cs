using Microsoft.AspNetCore.Mvc;

namespace Gamayun.UI.Utilities
{
    public class GamayunArea : AreaAttribute
    {
        public GamayunArea(string areaName) : base(areaName)
        {
            AreaName = areaName;
        }

        public string AreaName { get; }
    }
}
