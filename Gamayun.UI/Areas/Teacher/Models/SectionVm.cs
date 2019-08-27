using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Models
{
    public class SectionVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string TopicName { get; set; }
        public IEnumerable<PresenceVm> Presences { get; set; }
        public IEnumerable<string> Dates { get; internal set; }
    }
}
