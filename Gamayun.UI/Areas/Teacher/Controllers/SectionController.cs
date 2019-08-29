using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gamayun.Identity;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Command.Teacher;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.Infrastucture.Query.Teacher;
using Gamayun.UI.Areas.Teacher.Models;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var section = _dbContext.Sections
                .Include(x => x.PresenceDates)
                    .ThenInclude(x => x.Presences)
                        .ThenInclude(x => x.Student)
                            .ThenInclude(x => x.AppUser)
                .FirstOrDefault(x => x.ID == vm.Id);

            var studentGroupedPresences = section.PresenceDates.SelectMany(x => x.Presences.Select(y => new
            {
                Date = x.Date,
                DateId = x.ID,
                StudentId = y.StudentID,
                Student = y.Student.AppUser.FullName,
                WasPresent = y.WasPresent
            })).GroupBy(x => x.StudentId).ToList();

            var presencesVm = studentGroupedPresences.Select(x => {
                var studentData = x.Select(z => new { z.Student, z.StudentId }).FirstOrDefault();
                return new PresenceVm
                {
                    Student = studentData.Student,
                    StudentId = studentData.StudentId ?? 0,
                    StudentPresences = x.Select(y => (y.WasPresent, y.DateId)).ToList(),
                };
            });

            List<(string date, int presenceDateId)> datesVm = section.PresenceDates
                .OrderBy(x => x.Date)
                .Select(x => (x.Date.ToString("dd MMMM yyyy"), x.ID))
                .ToList();


            vm.Presences = presencesVm;
            vm.Dates = datesVm;

            return View(vm);
        }

        public ViewResult SectionCreate()
            => View(new SectionCreateVm
            {
                TopicGridConfiguration = new GridConfiguration<TopicRM>
                {
                    DataUrl = this.GetActionUrl<TopicController>(nameof(TopicController.TopicSearchQuery)),
                    PageSize = 3,
                },
                SemesterGridConfiguration = new GridConfiguration<SemesterRM>
                {
                    DataUrl = this.GetActionUrl(nameof(SemesterSearchQuery)),
                    PageSize = 3,
                },
                StudentGridConfiguration= new GridConfiguration<UserRM>
                {
                    DataUrl = this.GetActionUrl(nameof(StudentSearchQuery)),
                    PageSize = 3,
                },
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
        [HttpPost]
        public JsonResult SemesterSearchQuery([FromBody]GridFilters<SemesterRM> filters)
        => Json(_gridQueryRunner.Run(filters, new SemestersQueryHandler.Query()));

        [HttpPost]
        public JsonResult StudentSearchQuery([FromBody]GridFilters<UserRM> filters)
       => Json(_gridQueryRunner.Run(filters, new StudentsForSectionQueryHandler.Query()));

        [HttpPost]
        public ActionResult UpdatePresences(string data)
        {
            // [studentId:dateId:wasPresent]
            var temp = data; 
            return BadRequest();
        }
    }
}
