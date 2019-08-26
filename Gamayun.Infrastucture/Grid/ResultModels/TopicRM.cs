using System;
using System.Collections.Generic;
using System.Text;

namespace Gamayun.Infrastucture.Grid.ResultModels
{
    public class TopicRM : IGridResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
