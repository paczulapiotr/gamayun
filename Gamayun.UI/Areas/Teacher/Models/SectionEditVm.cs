using Gamayun.Infrastucture.Entities;
using Gamayun.Infrastucture.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Models
{
    public class SectionEditVm
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public SectionState State { get; set; }
        public IEnumerable<SectionStudentVm> Students { get; set; }
        public IGridConfiguration StudentsGridConfiguration { get; set; }
    }

    public class SectionStudentVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
