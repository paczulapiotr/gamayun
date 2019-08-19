using System.Collections.Generic;

namespace Gamayun.Infrastucture.Entities
{
    public class Student
    {
        public IEnumerable<StudentSection> StudentSections { get; set; }
    }
}