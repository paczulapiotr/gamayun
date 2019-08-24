using Gamayun.Infrastucture;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Gamayun.Infrastucture.Query;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.Infrastucture.Mapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.Infrastucture.Entities;
using Gamayun.Identity;
using System.Linq;
using AutoMapper;

namespace Tests
{

    [TestFixture]
    public class QueryHandlerTests
    {
        private GamayunDbContext _dbContext;
        private TeachersQueryHandler _queryHandler;
        private MapperConfiguration _mapperConfig;

        private static AppUser user1 = new AppUser { FirstName = "Mark", LastName = "Smith", Email = "marksmith@local.host" };
        private static AppUser user2 = new AppUser { FirstName = "Markov", LastName = "Smarkovsky", Email = "markovsmarkovsky@local.host" };
        private static AppUser user3 = new AppUser { FirstName = "Daniel", LastName = "Smirnov", Email = "danielsmirnov@local.host" };
        private static AppUser user4 = new AppUser { FirstName = "Anna", LastName = "Ann", Email = "annaann@local.host" };
        private static AppUser user5 = new AppUser { FirstName = "Jessica", LastName = "Brown", Email = "jessicabrown@local.host" };

        private static Teacher _teacher1 = new Teacher { AppUser = user1, AppUserID = user1.Id };
        private static Teacher _teacher2 = new Teacher { AppUser = user2, AppUserID = user2.Id };
        private static Teacher _teacher3 = new Teacher { AppUser = user3, AppUserID = user3.Id };
        private static Teacher _teacher4 = new Teacher { AppUser = user4, AppUserID = user4.Id };
        private static Teacher _teacher5 = new Teacher { AppUser = user5, AppUserID = user5.Id };
        private static Teacher[] _teachers => new Teacher[] { _teacher1, _teacher2, _teacher3, _teacher4, _teacher5 };

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GamayunDbContext>()
                  .UseInMemoryDatabase(databaseName: "TestDB")
                  .Options;
            _dbContext = new GamayunDbContext(options);
            _dbContext.Users.AddRange(new[] { user1, user2, user3, user4, user5 });
            _dbContext.Teachers.AddRange(new Teacher[] { _teacher1, _teacher2, _teacher3, _teacher4, _teacher5 });
            _dbContext.SaveChanges();
            _mapperConfig = AutomapperService.Initialize();
            _queryHandler = new TeachersQueryHandler(_dbContext, _mapperConfig);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }

        [TestCase("asc")]
        [TestCase("desc")]
        public void ShouldSort(string order)
        {
            // given
            var gridFilter = new GridFilters<UserRM>
            {
                PageIndex = 1,
                PageSize = 5,
                SortField = "Email",
                SortOrder = "asc",
            };

            // when
            var result = _queryHandler.Execute(gridFilter, new TeachersQueryHandler.Query());

            // then
            if (order == "asc")
            {
                CollectionAssert.AreEquivalent(
                    result.Data.Select(a => a.Id),
                    _teachers.OrderBy(x => x.AppUser.Email).Select(x => x.ID)
                    );
            }
            else if (order == "asc")
            {
                CollectionAssert.AreEquivalent(
                    result.Data.Select(a => a.Id),
                    _teachers.OrderByDescending(x => x.AppUser.Email).Select(x => x.ID)
                    );
            }
        }

        [Test]
        public void ShouldFilterString()
        {
            // given
            var gridFilter = new GridFilters<UserRM>
            {
                PageIndex = 1,
                PageSize = 5,
                SortField = "Email",
                SortOrder = "asc",
                Filters = new UserRM
                {
                    Id = 69,
                    Email = "",
                    FirstName = "Mark",
                    LastName = "",
                }
            };

            // when
            var result = _queryHandler.Execute(gridFilter, new TeachersQueryHandler.Query());

            // then
            Assert.AreEqual(result.ItemsCount, 2);
            CollectionAssert.AreEquivalent(
                result.Data.Select(a => a.Id),
                new[] { _teacher1, _teacher2 }.Select(a => a.ID)
                );
        }

    }
}