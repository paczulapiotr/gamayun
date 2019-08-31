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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            var presencesVm = studentGroupedPresences.Select(x =>
            {
                var studentData = x.Select(z => new { z.Student, z.StudentId }).FirstOrDefault();
                return new PresenceVm
                {
                    Student = studentData.Student,
                    StudentId = studentData.StudentId ?? 0,
                    StudentPresences = x.Select(y => (y.WasPresent, y.DateId)).ToList(),
                };
            }).OrderBy(x => x.Student);

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
            var vm = _dbContext.Sections
                .Include(x=>x.StudentSections)
                .ThenInclude(y=>y.Student)
                .ThenInclude(z=>z.AppUser)
                .Select(x =>
            new SectionEditVm
            {
                Id = x.ID,
                Name = x.Name,
                State = x.State,
                Students = x.StudentSections.Select(y => new SectionStudentVm { Id = y.StudentID, Name = y.Student.AppUser.FullName }),
            }).FirstOrDefault(x => x.Id == id);
            if (vm == null)
            {
                return ErrorResult();
            }

            vm.StudentsGridConfiguration = new GridConfiguration<UserRM>
            {
                DataUrl = GetActionUrl(nameof(StudentSearchQuery)),
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult SectionEdit(SectionEditDTO dto)
        {
            var command = new EditSectionCommandHandler.Command
            {
                Id = dto.Id,
                Name = dto.Name,
                State = dto.State,
                StudentIds = string.IsNullOrWhiteSpace(dto.StudentIds) 
                ? new List<int>() 
                : dto.StudentIds.Split(',').Select(x => int.Parse(x)).ToList(),
            };

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
        public ActionResult UpdatePresences(string data, int sectionId)// data = [studentId:dateId:wasPresent]
        {
            var regex = new Regex(@"(\d):(\d):(false|true)", RegexOptions.IgnoreCase);
            (int studentId, int dateId, bool wasPresent)[] tuple = data.Split(',').Select(x => {
                var groups = regex.Match(x).Groups;
                return
                    (int.Parse(groups[1].Value),
                    int.Parse(groups[2].Value),
                    bool.Parse(groups[3].Value.ToLower()));
                }).ToArray();

            var result = _commandRunner.Run(new UpdateSectionPresencesCommandHandler.Command(tuple, sectionId));

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SectionView), new { id = sectionId });
            }

            return Error();
        }

        [HttpPost]
        public ActionResult ChangeGrade(SectionGradeDTO dto)
        {
            var section = _dbContext.Sections.FirstOrDefault(x => x.ID == dto.Id);
            if (section == null || dto.Grade == null 
                || dto.Grade < 2 || dto.Grade > 5)
            {
                return Error();
            }

            section.Grade = dto.Grade;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(SectionView), new { id = dto.Id });

        }
    }
}
