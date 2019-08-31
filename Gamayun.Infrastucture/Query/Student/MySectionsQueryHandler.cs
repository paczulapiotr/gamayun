using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamayun.Infrastucture.Query.Student
{
    public class MySectionsQueryHandler : GridQueryHandler<SectionRM, MySectionsQueryHandler.Query>
    {
        public MySectionsQueryHandler(GamayunDbContext dbContext, MapperConfiguration mapperConfiguration) : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<SectionRM> Execute(GridFilters<SectionRM> filters, Query query)
        {
            var sections = _dbContext.Sections
                .Include(x => x.StudentSections)
                .Where(x => x.StudentSections.Any(y => y.StudentID == query.StudentId));
            return Result(filters, sections);
        }

        public class Query : IGridQuery 
        {
            public int StudentId { get; set; }
        }
    }
}
