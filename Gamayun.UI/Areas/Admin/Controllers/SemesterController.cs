using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Command.Admin;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.UI.Areas.Admin.Models;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Admin.Controllers
{
    public class SemesterController : AdminController
    {
        private readonly GamayunDbContext _dbContext;
        private readonly MapperConfiguration _mapperConfiguration;

        public SemesterController(
            GamayunDbContext dbContext,
            MapperConfiguration mapperConfiguration,
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings)
            : base(commandRunner, gridQueryRunner, settings)
        {
            _dbContext = dbContext;
            _mapperConfiguration = mapperConfiguration;
        }
        public ActionResult SemesterView(int id)
        {
            var vm = _dbContext.Semesters
                .ProjectTo<SemesterVm>(_mapperConfiguration)
                .FirstOrDefault(x => x.ID == id);
            
            if (vm == null)
            {
                return ErrorResult();
            }
            return View(vm);
        }

        public ViewResult SemesterCreate() => View();
        [HttpPost]
        public ActionResult SemesterCreate(CreateSemesterCommandHandler.Command command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SemesterSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();
        }

        public ActionResult SemesterEdit(int id)
        {
            var vm = _dbContext.Semesters.Select(x =>
           new EditSemesterCommandHandler.Command
           {
               ID = x.ID,
               Major = x.Major,
               FinishedOn = x.FinishedOn
           }).FirstOrDefault(x => x.ID == id);
            if (vm == null)
            {
                return ErrorResult();
            }
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult SemesterEdit(EditSemesterCommandHandler.Command command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SemesterSearch));
            }

            ViewBag.Errors = result.Errors;
            return View();
        }

        public ViewResult SemesterSearch()
             => View(new GridConfiguration<SemesterRM>
             {
                 DataUrl = GetActionUrl(nameof(SemesterSearchQuery)),
                 SelectHref = GetActionUrl(nameof(SemesterView))
             });

        [HttpPost]
        public JsonResult SemesterSearchQuery([FromBody]GridFilters<SemesterRM> filter)
            => Json(_gridQueryRunner.Run(filter, new SemestersQueryHandler.Query()));

        [HttpPost]
        public ActionResult SemesterObsolete(int id)
        {
            var semester = _dbContext.Semesters.FirstOrDefault(x => x.ID == id);
            if (semester == null)
            {
                return ErrorResult();
            }
            semester.IsObsolete = true;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(SemesterView), new { id });
        }

        [HttpPost]
        public ActionResult SemesterRestore(int id)
        {
            var semester = _dbContext.Semesters.FirstOrDefault(x => x.ID == id);
            if (semester == null)
            {
                return ErrorResult();
            }
            semester.IsObsolete = false;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(SemesterView), new { id });
        }
    }
}
