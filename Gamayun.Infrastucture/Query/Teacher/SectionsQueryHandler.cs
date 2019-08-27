using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using System.Collections.Generic;
using System.Linq;

namespace Gamayun.Infrastucture.Query.Teacher
{
    public class SectionsQueryHandler : GridQueryHandler<SectionRM, SectionsQueryHandler.Query>
    {
        public SectionsQueryHandler(
            GamayunDbContext dbContext, 
            MapperConfiguration mapperConfiguration) 
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<SectionRM> Execute(GridFilters<SectionRM> filters, Query query)
        {
            var sections = _dbContext.Sections.Join(
                _dbContext.Topics.Where(t => t.TeacherID == query.TeacherID),
                x => x.TopicID,
                y => y.ID,
                (sec, top) => sec);

            return Result(filters, sections);
        }

        public class Query : IGridQuery
        {
            public int TeacherID { get; set; }
        }
    }
}
