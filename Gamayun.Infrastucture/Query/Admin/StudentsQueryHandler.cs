using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Microsoft.EntityFrameworkCore;

namespace Gamayun.Infrastucture.Query.Admin
{
    public class StudentsQueryHandler : GridQueryHandler<UserRM, StudentsQueryHandler.Query>
    {
        public StudentsQueryHandler(
            GamayunDbContext dbContext,
            MapperConfiguration mapperConfiguration)
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<UserRM> Execute(GridFilters<UserRM> filters, Query query)
        {
            var teachers = _dbContext.Students
                .Include(a => a.AppUser);

            return Result(filters, teachers);
        }

        public class Query : IGridQuery
        {

        }
    }
}
