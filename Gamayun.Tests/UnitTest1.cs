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

namespace Tests
{
    public class Tests
    {
        private GamayunDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GamayunDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDB")
               .Options;
            _dbContext = new GamayunDbContext(options);
        }
        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public void Test1()
        {
            var user1 = new AppUser
            {
                FirstName = "Mark",
                LastName="Smith",
                Email="bbbb@local.host"
            };
            var user2 = new AppUser
            {
                FirstName = "Markov",
                LastName = "Smarkovsky",
                Email = "aaaa@local.host"
            };
            var user3 = new AppUser
            {
                FirstName = "Daniel",
                LastName = "Smirnov",
                Email = "daniel.smirnov@local.host"
            };
            _dbContext.Users.AddRange(new[] { user1, user2, user3 });
            _dbContext.Teachers.AddRange(new Teacher[] {
                new Teacher
                {
                    AppUser = user1,
                    AppUserID = user1.Id,
                },
                new Teacher
                {
                    AppUser = user2,
                    AppUserID = user2.Id,
                },
                new Teacher
                {
                    AppUser = user3,
                    AppUserID = user3.Id,
                }
            });
            _dbContext.SaveChanges();
            var mapperConfig = AutomapperService.Initialize();
            TeachersQueryHandler handler = new TeachersQueryHandler(_dbContext, mapperConfig);
            var gridFilter = new GridFilters<UserRM> {
                PageIndex = 1,
                PageSize = 5,
                SortField = "Email",
                SortOrder = "asc",
                Filters = new UserRM
                {
                    Id = 69,
                    Email = "",
                    FirstName = "Mark",
                    LastName="",
                }
            };
            var result = handler.Execute(gridFilter, new TeachersQueryHandler.Query());
            
            Assert.AreEqual(result.ItemsCount, 2);
            CollectionAssert.AreEquivalent(
                result.Data.Select(a => a.Id), 
                result.Data.Select(a => a.Id)
                );
        }
    }
}