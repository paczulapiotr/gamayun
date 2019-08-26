using Gamayun.Identity;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Teacher;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Controllers
{
    public class SectionController : TeacherController
    {
        public SectionController(
        GamayunDbContext dbContext,
        UserManager<AppUser>
        userManager,
        ICommandRunner commandRunner,
        IGridQueryRunner gridQueryRunner,
        ISettings settings)
        : base(dbContext, userManager, commandRunner, gridQueryRunner, settings)
        {
        }

        public ViewResult SectionView() => View("_Home");
        public ViewResult SectionCreate() => View("_Home");
        [HttpPost]
        public ViewResult SectionCreate() => View("_Home");
        public ViewResult SectionEdit(int id) => View("_Home");
        [HttpPost]
        public ViewResult SectionEdit() => View("_Home");
        public ViewResult SectionSearch()
             => View(new GridConfiguration<SectionRM>
             {
                 DataUrl = GetActionUrl(nameof(SectionSearchQuery)),
                 SelectHref = GetActionUrl(nameof(SectionView))
             });
        [HttpPost]
        public async Task<JsonResult> SectionSearchQuery([FromBody]GridFilters<SectionRM> filter)
            => Json(_gridQueryRunner.Run(filter, new SectionsQueryHandler.Query
            {
                TeacherID = await GetTeacherId() ?? 0
            }));

    }
}
