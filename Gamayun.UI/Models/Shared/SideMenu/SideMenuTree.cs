using System.Collections.Generic;

namespace Gamayun.UI.Models
{
    public class SideMenuTree
    {
        public IEnumerable<SideMenuLeaf> Leaves { get; set; } = new List<SideMenuLeaf>();
    }
}
