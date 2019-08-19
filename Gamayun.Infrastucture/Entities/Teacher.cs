using Gamayun.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamayun.Infrastucture.Entities
{
    public class Teacher : UserWithRole
    {
        public IEnumerable<Topic> Topics { get; set; }
    }
}