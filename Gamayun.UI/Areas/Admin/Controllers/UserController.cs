using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        public UserController(IGridQueryRunner queryRunner, ISettings settings) : base(queryRunner, settings)
        {
        }

        public ViewResult AdminSearch()
             => View(new GridConfiguration<UserRM>
             {
                 DataUrl = GetActionUrl(nameof(AdminSearchQuery)),
             });

        [HttpPost]
        public JsonResult AdminSearchQuery(GridFilters<UserRM> filter, AdminsQueryHandler.Query query)
            => Json(_queryRunner.Run<UserRM, AdminsQueryHandler.Query>(filter, query));

        public ViewResult TeacherSearch()
            => View(new GridConfiguration<UserRM>
            {
                DataUrl = GetActionUrl(nameof(TeacherSearchQuery)),
            });

        [HttpPost]
        public JsonResult TeacherSearchQuery(GridFilters<UserRM> filter, TeachersQueryHandler.Query query)
           => Json(_queryRunner.Run<UserRM, TeachersQueryHandler.Query>(filter, query));

        public ViewResult StudentSearch()
            => View(new GridConfiguration<UserRM>
            {
                DataUrl = GetActionUrl(nameof(StudentSearchQuery)),
            });

        [HttpPost]
        public JsonResult StudentSearchQuery(GridFilters<UserRM> filter, StudentsQueryHandler.Query query)
           => Json(_queryRunner.Run<UserRM, StudentsQueryHandler.Query>(filter, query));

    }
}
