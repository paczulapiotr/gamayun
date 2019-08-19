using Gamayun.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamayun.Infrastucture.Entities
{
    public abstract class UserWithRole : Entity
    {
        [ForeignKey(nameof(AppUserID))]
        public AppUser AppUser { get; set; }
        public string AppUserID { get; set; }
    }
}
