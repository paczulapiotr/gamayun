using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Areas.Admin.Models
{
    public class SemesterVm
    {
        public int ID { get; set; }
        public string Major { get; set; }

        public string CreatedOn { get; set; }

        public string FinishedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsObsolete { get; set; }
    }
}
