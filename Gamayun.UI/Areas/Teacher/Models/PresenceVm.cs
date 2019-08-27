using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Teacher.Models
{
    public class PresenceVm
    {
        public int StudentId { get; set; }
        public string Student { get; set; }
        public List<bool> StudentPresences { get; set; } = new List<bool>();
    }
}
