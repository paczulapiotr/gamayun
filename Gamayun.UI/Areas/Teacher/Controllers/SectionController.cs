using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gamayun.Identity;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Command.Teacher;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Teacher;
using Gamayun.UI.Areas.Teacher.Models;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Controllers
{
    public class SectionController : TeacherController
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public SectionController(
        GamayunDbContext dbContext,
        MapperConfiguration mapperConfiguration,
        UserManager<AppUser>
        userManager,
        ICommandRunner commandRunner,
        IGridQueryRunner gridQueryRunner,
        ISettings settings)
        : base(dbContext, userManager, commandRunner, gridQueryRunner, settings)
        {
            _mapperConfiguration = mapperConfiguration;
        }

        public ActionResult SectionView(int id)
        {
            var vm = _dbContext.Sections
                .ProjectTo<SectionVm>(_mapperConfiguration)
                .FirstOrDefault(x => x.Id == id);

            if (vm == null)
            {
                return ErrorResult();
            }
            return View(vm);
        }

        public ViewResult SectionCreate()
            => View(new SectionCreateVm
            {
                GridConfiguration = new GridConfiguration<TopicRM>
                {
                    DataUrl = this.GetActionUrl<TopicController>(nameof(TopicController.TopicSearchQuery)),
                }
            });
        [HttpPost]
        public ActionResult SectionCreate(CreateSectionCommandHandler.Command command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SectionSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();
        }
        public ActionResult SectionEdit(int id)
        {
            var vm = _dbContext.Sections.Select(x =>
            new EditSectionCommandHandler.Command
            {
               Id = x.ID,
               Name = x.Name,
               State = x.State,
            }).FirstOrDefault(x => x.Id == id);
            if (vm == null)
            {
                return ErrorResult();
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult SectionEdit(EditSectionCommandHandler.Command command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SectionSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();
        }

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
