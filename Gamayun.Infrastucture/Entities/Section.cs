using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamayun.Infrastucture.Entities
{
    public class Section : Entity
    {
        public string Name { get; set; }
        public SectionState State{ get; set; }
        public int? TopicID { get; set; }
        [ForeignKey(nameof(TopicID))]
        public Topic Topic { get; set; }
        public IEnumerable<StudentSection> StudentSections { get; set; }
        public IEnumerable<PresenceDate> PresenceDates { get; set; }
    }
}
