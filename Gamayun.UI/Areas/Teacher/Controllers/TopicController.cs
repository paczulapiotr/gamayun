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
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Controllers
{
    public class TopicController : TeacherController
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public TopicController(
            MapperConfiguration mapperConfiguration,
            GamayunDbContext dbContext, 
            UserManager<AppUser> userManager, 
            ICommandRunner commandRunner, 
            IGridQueryRunner gridQueryRunner, 
            ISettings settings) 
            : base(dbContext, userManager, commandRunner, gridQueryRunner, settings)
        {
            _mapperConfiguration = mapperConfiguration;
        }

        public ViewResult TopicSearch()
            => View(new GridConfiguration<TopicRM>
            {
                DataUrl = GetActionUrl(nameof(TopicSearchQuery)),
                SelectHref = GetActionUrl(nameof(TopicView))
            });


        [HttpPost]
        public async Task<JsonResult> TopicSearchQuery([FromBody]GridFilters<TopicRM> filter)
              => Json(_gridQueryRunner.Run(filter, new TopicsQueryHandler.Query
              {
                  TeacherId = await GetTeacherId() ?? 0,
              }));

        public async Task<ActionResult> TopicView(int id)
        {
            try
            {
                var teacherId = await GetTeacherId();
                var topicVm = _dbContext.Topics
                        .Include(x => x.Teacher)
                        .Where(x => x.Teacher.ID == teacherId)
                        .ProjectTo<TopicVm>(_mapperConfiguration)
                        .FirstOrDefault(x => x.Id == id);
                return View(topicVm);
            }
            catch(Exception ex)
            {
                return Error();
            }
        }

        public async Task<ActionResult> TopicEdit(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var teacherId = _dbContext.Teachers.FirstOrDefault(x => x.AppUserID == user.Id)?.ID;
                var topicVm = _dbContext.Topics
                        .Include(x => x.Teacher)
                        .Where(x => x.Teacher.ID == teacherId)
                        .Select(x=>new EditTopicCommandHandler.Command
                        {
                            Id = x.ID,
                            Name = x.Name,
                            Description = x.Description,
                        })
                        .FirstOrDefault(x => x.Id == id);
                return View(topicVm);
            }
            catch
            {
                return Error();
            }
        }

        [HttpPost]
        public ActionResult TopicEdit(EditTopicCommandHandler.Command command)
        {
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(TopicSearch));
            }

            return Error();
        }

        public ActionResult TopicCreate() => View();

        [HttpPost]
        public async Task<ActionResult> TopicCreate(CreateTopicCommandHandler.Command command)
        {
            var teacherId = await GetTeacherId();
            if (teacherId == null)
                return Error();

            command.TeacherID = teacherId.Value;
            var result = _commandRunner.Run(command);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(TopicSearch));
            }

            return Error();
        }
    }
}
