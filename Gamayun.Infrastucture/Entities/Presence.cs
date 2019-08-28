using System.ComponentModel.DataAnnotations.Schema;

namespace Gamayun.Infrastucture.Entities
{
    public class Presence : Entity
    {
        public int? StudentID { get; set; }
        [ForeignKey(nameof(StudentID))]
        public Student Student { get; set; }
        public bool WasPresent { get; set; }
    }
}