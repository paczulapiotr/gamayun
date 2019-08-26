using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;

namespace Gamayun.Infrastucture.Query.Admin
{
    public class SemestersQueryHandler : GridQueryHandler<SemesterRM, SemestersQueryHandler.Query>
    {
        public SemestersQueryHandler(
            GamayunDbContext dbContext, 
            MapperConfiguration mapperConfiguration) 
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<SemesterRM> Execute(GridFilters<SemesterRM> filters, Query query)
        {
            var semesters= _dbContext.Semesters;

            return Result(filters, semesters);
        }

        public class Query : IGridQuery
        {

        }
    }
}
