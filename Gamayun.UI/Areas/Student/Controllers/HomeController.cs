
using Gamayun.UI.Controllers;
using Gamayun.UI.Models;
using Gamayun.UI.Models.Shared.Grid;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Gamayun.UI.Areas.Student.Controllers
{
    public class MyClass
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
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

        [HttpPost]
        public JsonResult Values([FromBody]GridFilters<MyClass> filters)
        {
            var vm = new GridResult<MyClass>
            {
                ItemsCount = 100,
                Data = new MyClass[]
                {
                    new MyClass{ Age = 19, Name="Janek Moniek"},
                    new MyClass{ Age = 20, Name="Anina Moniek"},   
                    new MyClass{ Age = 21, Name="Janek Moniek"},
                    new MyClass{ Age = 22, Name="Anina Moniek"},   
                    new MyClass{ Age = 23, Name="Janek Moniek"},
                    new MyClass{ Age = 24, Name="Anina Moniek"},   
                    new MyClass{ Age = 22, Name="Janek Moniek"},
                    new MyClass{ Age = 22, Name="Anina Moniek"},   
                    new MyClass{ Age = 19, Name="Janek Moniek"},
                    new MyClass{ Age = 20, Name="Anina Moniek"},   
                    new MyClass{ Age = 25, Name="Janek Moniek"},
                    new MyClass{ Age = 26, Name="Anina Moniek"},   
                    new MyClass{ Age = 27, Name="Janek Moniek"},
                    new MyClass{ Age = 20, Name="Anina Moniek"},
                    new MyClass{ Age = 33, Name="Anina Moniek"},
                }
            };

            return Json(vm);
        }
    }
}
