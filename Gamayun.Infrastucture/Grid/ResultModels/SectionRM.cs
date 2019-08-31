using System;
using System.Collections.Generic;
using System.Text;

namespace Gamayun.Infrastucture.Grid.ResultModels
{
    public class SectionRM : IGridResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string TopicName { get; set; }
    }
}
