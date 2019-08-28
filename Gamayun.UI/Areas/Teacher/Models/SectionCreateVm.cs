using Gamayun.Infrastucture.Grid;
using Gamayun.Infrastucture.Grid.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Models
{
    public class SectionCreateVm
    {
        public GridConfiguration<TopicRM> TopicGridConfiguration { get; set; }
        public GridConfiguration<SemesterRM> SemesterGridConfiguration { get; set; }
        public GridConfiguration<UserRM> StudentGridConfiguration { get; set; }
    }
}
