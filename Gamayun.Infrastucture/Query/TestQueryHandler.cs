using AutoMapper;
using Gamayun.Infrastucture.Grid;

namespace Gamayun.Infrastucture.Query
{
    public class MyClass : IGridResultModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id => 69;
    }

    public class TestQueryHandler : GridQueryHandler<MyClass, TestQueryHandler.Query>
    {
        public TestQueryHandler(GamayunDbContext dbContext, MapperConfiguration mapperConfiguration) : base(dbContext, mapperConfiguration)
        {
        }

        private static MyClass[] data = new MyClass[]
              {
                  new MyClass { Age = 1, Name = "Janek Moniek" },
                  new MyClass { Age = 2, Name = "Janek Moniek" },
                  new MyClass { Age = 3, Name = "Janek Moniek" },
                  new MyClass { Age = 4, Name = "Janek Moniek" },
                  new MyClass { Age = 5, Name = "Janek Moniek" },
                  new MyClass { Age = 6, Name = "Janek Moniek" },
                  new MyClass { Age = 7, Name = "Janek Moniek" },
                  new MyClass { Age = 8, Name = "Janek Moniek" },
                  new MyClass { Age = 9, Name = "Janek Moniek" },
                  new MyClass { Age = 10, Name = "Janek Moniek" },
                  new MyClass { Age = 11, Name = "Janek Moniek" },
                  new MyClass { Age = 12, Name = "Janek Moniek" },
                  new MyClass { Age = 13, Name = "Janek Moniek" },
                  new MyClass { Age = 14, Name = "Janek Moniek" },
                  new MyClass { Age = 15, Name = "Janek Moniek" },
                  new MyClass { Age = 16, Name = "Janek Moniek" },
                  new MyClass { Age = 17, Name = "Janek Moniek" },
                  new MyClass { Age = 18, Name = "Janek Moniek" },
                  new MyClass { Age = 19, Name = "Janek Moniek" },
                  new MyClass { Age = 20, Name = "Anina Moniek" },
                  new MyClass { Age = 21, Name = "Janek Moniek" },
                  new MyClass { Age = 22, Name = "Anina Moniek" },
                  new MyClass { Age = 23, Name = "Janek Moniek" },
                  new MyClass { Age = 24, Name = "Anina Moniek" },
                  new MyClass { Age = 25, Name = "Janek Moniek" },
                  new MyClass { Age = 26, Name = "Anina Moniek" },
                  new MyClass { Age = 27, Name = "Janek Moniek" },
                  new MyClass { Age = 28, Name = "Anina Moniek" },
                  new MyClass { Age = 29, Name = "Janek Moniek" },
                  new MyClass { Age = 31, Name = "Anina Moniek" },
                  new MyClass { Age = 32, Name = "Janek Moniek" },
                  new MyClass { Age = 33, Name = "Anina Moniek" },
                  new MyClass { Age = 34, Name = "Anina Moniek" },
              };
        private int itemsCount = data.Length;

        public override GridResult<MyClass> Execute(GridFilters<MyClass> filters, Query query)
        {
            var array = ApplyPagination(filters, data);
            return new GridResult<MyClass>
            {
                ItemsCount = itemsCount,
                Data = array
            };
        }

        public class Query : IGridQuery
        {
        }
    }
}
