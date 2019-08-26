using System;
using System.Collections.Generic;
using System.Text;

namespace Gamayun.Infrastucture.Grid.ResultModels
{
    public class SemesterRM : IGridResultModel
    {
        public int Id { get; set; }
        public string Major { get; set; }

        [PropertyTitle("Created On [yy/mm/dd HH:MM]")]
        public string CreatedOn { get; set; }

        [PropertyTitle("Finished On [yy/mm/dd]")]
        public string FinishedOn { get; set; }

        [DontFilter]
        public bool IsActive { get; set; }

        [DontFilter]
        public bool IsObsolete { get; set; }
    }
}
