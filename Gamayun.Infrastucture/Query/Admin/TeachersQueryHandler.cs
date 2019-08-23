using AutoMapper;
using Gamayun.Infrastucture.Entities;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gamayun.Infrastucture.Query.Admin
{
    public class TeachersQueryHandler : GridQueryHandler<UserRM, TeachersQueryHandler.Query>
    {
        public TeachersQueryHandler(
            GamayunDbContext dbContext, 
            MapperConfiguration mapperConfiguration) 
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<UserRM> Execute(GridFilters<UserRM> filters, Query query)
        {
            var teachers = _dbContext.Teachers
                .Include(a => a.AppUser);

            return Result(filters, teachers);
        }

        public class Query : IGridQuery
        {

        }
    }
}
