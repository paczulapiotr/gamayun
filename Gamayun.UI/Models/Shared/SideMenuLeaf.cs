using System.Collections.Generic;

namespace Gamayun.UI.Models
{
    public class SideMenuLeaf
    {
        public string HeaderName { get; set; }
        public IEnumerable<SideMenuCategory> Categories { get; set; } = new List<SideMenuCategory>();
    }
}
