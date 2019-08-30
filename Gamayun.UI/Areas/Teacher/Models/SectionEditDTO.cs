using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamayun.Infrastucture.Entities;

namespace Gamayun.UI.Areas.Teacher.Models
{
    public class SectionEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SectionState State { get; set; }
        public string StudentIds { get; set; }
    }
}
