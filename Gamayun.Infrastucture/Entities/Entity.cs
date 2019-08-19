using System.ComponentModel.DataAnnotations;

namespace Gamayun.Infrastucture.Entities
{
    public abstract class Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
