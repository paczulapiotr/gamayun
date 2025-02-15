﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamayun.Infrastucture.Entities
{
    public class Section : Entity
    {
        public string Name { get; set; }

        [Range(2, 5)]
        public int? Grade { get; set; }

        public SectionState State{ get; set; }
        
        public int? TopicID { get; set; }
        
        [ForeignKey(nameof(TopicID))]
        public Topic Topic { get; set; }
        
        public int? SemesterID { get; set; }
        [ForeignKey(nameof(SemesterID))]
        public Semester Semester { get; set; }
        
        public IEnumerable<StudentSection> StudentSections { get; set; }
 
        public IEnumerable<PresenceDate> PresenceDates { get; set; }
    }
}
