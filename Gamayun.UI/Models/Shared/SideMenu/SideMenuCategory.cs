using System.Collections.Generic;

namespace Gamayun.UI.Models
{
    public class SideMenuCategory
    {
        public string Icon { get; set; }
        public string CategoryName { get; set; }
        public string AnchorHref { get; set; }
        public bool HasChildren => string.IsNullOrWhiteSpace(AnchorHref);
        public IEnumerable<SideMenuCategoryOption> Options { get; set; } = new List<SideMenuCategoryOption>();
    }
}
