using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Student.Models
{
    public class StudentSectionVm
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string TopicName { get; set; }
    }
}
