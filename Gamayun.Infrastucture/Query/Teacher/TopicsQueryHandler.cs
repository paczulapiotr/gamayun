using AutoMapper;
using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using System.Linq;

namespace Gamayun.Infrastucture.Query.Teacher
{
    public class TopicsQueryHandler : GridQueryHandler<TopicRM, TopicsQueryHandler.Query>
    {
        public TopicsQueryHandler(
            GamayunDbContext dbContext, 
            MapperConfiguration mapperConfiguration) 
            : base(dbContext, mapperConfiguration)
        {
        }

        public override GridResult<TopicRM> Execute(GridFilters<TopicRM> filters, Query query)
        {
            var topics = _dbContext.Topics.Where(x=>x.TeacherID == query.TeacherId);
            return Result(filters, topics);
        }

        public class Query : IGridQuery
        {
            public int TeacherId { get; set; }
        }
    }
}
