using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamayun.Infrastucture.Entities
{
    public class Topic : Entity
    {
        public int? TeacherID { get; set; }
        [ForeignKey(nameof(TeacherID))]
        public Teacher Teacher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Section> Sections { get; set; } = new List<Section>();
    }

    public class TopicEntityTypeConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasMany(a => a.Sections)
                .WithOne(x => x.Topic)
                .HasForeignKey(a => a.TopicID);
        }
    }
}
