using Gamayun.Identity;
using Gamayun.Infrastucture;
using Gamayun.Infrastucture.Command;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Student;
using Gamayun.UI.Controllers;
using Gamayun.UI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Gamayun.Infrastucture.Entities;
using Gamayun.UI.Areas.Student.Models;

namespace Gamayun.UI.Areas.Student.Controllers
{
    public class SectionController : StudentController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly GamayunDbContext _dbContext;
        private readonly MapperConfiguration _mapperConfiguration;

        public SectionController(
        UserManager<AppUser> userManager,
        GamayunDbContext dbContext,
        MapperConfiguration mapperConfiguration,
        ICommandRunner commandRunner,
        IGridQueryRunner gridQueryRunner,
        ISettings settings)
        : base(commandRunner, gridQueryRunner, settings)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapperConfiguration = mapperConfiguration;
        }

        public ViewResult FindSections()
            => View(new GridConfiguration<SectionRM>
            {
                DataUrl = GetActionUrl(nameof(FindSectionsQuery)),
            });

        [HttpPost]
        public JsonResult FindSectionsQuery([FromBody]GridFilters<SectionRM> filter)
            => Json(_gridQueryRunner.Run(filter, new FindSectionsQueryHandler.Query 
            {
                StudentId = GetStudentId().Result ?? -1
            }));


        public ActionResult MySections()
            => View(new GridConfiguration<SectionRM>
            {
                DataUrl = GetActionUrl(nameof(MySectionsQuery)),
                SelectHref = GetActionUrl(nameof(SectionView))
            });

        [HttpPost]
        public JsonResult MySectionsQuery([FromBody]GridFilters<SectionRM> filter)
                  => Json(_gridQueryRunner.Run(filter, new MySectionsQueryHandler.Query
                  {
                      StudentId = GetStudentId().Result ?? -1
                  }));

        public async  Task<ActionResult> SectionView(int id)
        {
            var studentId = await GetStudentId();
            if (studentId == null)
                return Error();

            var vm = _dbContext.Sections
                .Include(x => x.StudentSections)
                .Where(x => x.StudentSections.Any(y => y.StudentID == studentId))
                .ProjectTo<StudentSectionVm>(_mapperConfiguration).FirstOrDefault(x => x.Id == id);
            if (vm == null)
                return Error();

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInOutDTO dto)
        {
            var studentId = await GetStudentId();
            if (studentId == null)
                return Error();

            var section = _dbContext.Sections
                .Include(x => x.StudentSections)
                .FirstOrDefault(x => x.ID == dto.Id);

            if (section == null || section.State != SectionState.Active) 
                return Error();

            if (_dbContext.StudentSections.Any(x => x.StudentID == studentId && x.SectionID == dto.Id))
                return Error();

            _dbContext.StudentSections.Add(new StudentSection
            {
                SectionID = dto.Id,
                StudentID = studentId.Value
            });

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(SectionView), new { id = dto.Id });
        }

        [HttpPost]
        public async Task<ActionResult> SignOut(SignInOutDTO dto)
        {
            var studentId = await GetStudentId();
            if (studentId == null)
                return Error();
            var studentSection = _dbContext.StudentSections
                .Where(x => x.SectionID == dto.Id)
                .Where(x => x.StudentID == studentId)
                .FirstOrDefault();
            
            if (studentSection == null)
                return Error();

            _dbContext.Entry(studentSection).State = EntityState.Deleted;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(FindSections));
        }

        private async Task<int?> GetStudentId()
        {
            var user = await _userManager.GetUserAsync(User);
            return _dbContext.Students.FirstOrDefault(x => x.AppUserID == user.Id)?.ID;
        }

    }
}
