using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamayun.Infrastucture.Entities
{
    public class Section
    {
        public SectionState State{ get; set; }
        public int? TopicID { get; set; }
        public Topic Topic { get; set; }
        public IEnumerable<StudentSection> StudentSections { get; set; }
        public IEnumerable<PresenceDate> PresenceDates { get; set; }
    }
}
