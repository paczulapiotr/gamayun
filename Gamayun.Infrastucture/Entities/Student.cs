using Gamayun.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamayun.Infrastucture.Entities
{
    public class Student : UserWithRole
    {
        public IEnumerable<StudentSection> StudentSections { get; set; }
    }
}