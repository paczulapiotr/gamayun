using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gamayun.Infrastucture.Query.Admin
{
    public class StudentsForSectionQueryHandler : GridQueryHandler<UserRM, StudentsForSectionQueryHandler.Query>
    {
        public StudentsForSectionQueryHandler(
            GamayunDbContext dbContext,
            MapperConfiguration mapperConfiguration)
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<UserRM> Execute(GridFilters<UserRM> filters, Query query)
        {
            var students = _dbContext.Students
                .Include(a => a.AppUser)
                .Where(x => !x.AppUser.IsObsolete);

            return Result(filters, students);
        }


        public class Query : IGridQuery
        {

        }
    }
}
