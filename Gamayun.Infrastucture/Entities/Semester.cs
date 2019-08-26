using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gamayun.Infrastucture.Entities
{
    public class Semester : Entity
    {
        public string Major { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime FinishedOn { get; set; }
        
        [NotMapped]
        public bool IsActive => FinishedOn >= DateTime.Now && !IsObsolete;
       
        public bool IsObsolete{ get; set; }
    }
}
