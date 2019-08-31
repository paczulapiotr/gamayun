using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Microsoft.EntityFrameworkCore;

namespace Gamayun.Infrastucture.Query.Admin
{
    public class AdminsQueryHandler : GridQueryHandler<UserRM, AdminsQueryHandler.Query>
    {
        public AdminsQueryHandler(
            GamayunDbContext dbContext,
            MapperConfiguration mapperConfiguration)
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<UserRM> Execute(GridFilters<UserRM> filters, Query query)
        {
            var admins = _dbContext.Admins
                .Include(a => a.AppUser);

            return Result(filters, admins);
        }

        public class Query : IGridQuery
        {

        }
    }
}
