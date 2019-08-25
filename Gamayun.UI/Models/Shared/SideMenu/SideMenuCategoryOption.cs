namespace Gamayun.UI.Models
{
    public class SideMenuCategoryOption
    {
        public SideMenuCategoryOption()
        {}

        public SideMenuCategoryOption(string optionName, string anchorHref)
        {
            OptionName = optionName;
            AnchorHref = anchorHref;
        }

        public string OptionName { get; set; }
        public string AnchorHref { get; set; }
    }
}
