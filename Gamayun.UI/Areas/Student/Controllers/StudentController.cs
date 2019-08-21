
using Gamayun.UI.Controllers;
using Gamayun.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Gamayun.UI.Areas.Student.Controllers
{
    public class HomeController : StudentController
    {
        public ViewResult Index()
        {
            ViewBag.SideMenuTree = new SideMenuTree
            {
                Leaves = new List<SideMenuLeaf>()
                {
                    new SideMenuLeaf
                    {
                        HeaderName = "Main menu",
                        Categories = new List<SideMenuCategory>
                        {
                            new SideMenuCategory
                            {
                                CategoryName = "Majors",
                                Options = new List<SideMenuCategoryOption>()
                                {
                                    new SideMenuCategoryOption
                                    {
                                        OptionName = "Search",
                                        AnchorHref = "/majors/search"
                                    },
                                    new SideMenuCategoryOption
                                    {
                                        OptionName = "Create new",
                                        AnchorHref = "/majors/create"
                                    }
                                }
                            },
                            new SideMenuCategory
                            {
                                AnchorHref = "#",
                                CategoryName = "Direct link bro"
                            }
                        }
                    },
                    new SideMenuLeaf
                    {
                        HeaderName = "Users",
                        Categories = new List<SideMenuCategory>
                        {
                            new SideMenuCategory
                            {
                                CategoryName = "Direct category",
                                AnchorHref="/test/href",
                                Icon="",
                            }
                        }
                    }
                }
            };
            return View();
        }
    }
}
