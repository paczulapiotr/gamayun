using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.Infrastucture.Query
{
    public interface IQuery
    {
    }

    public interface IQueryDispatcher<TQuery> where TQuery : IQuery
    {

    }
}
