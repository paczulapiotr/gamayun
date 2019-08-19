using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamayun.Infrastucture.Entities
{
    public class Topic
    {
        public Teacher Teacher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Section> Sections { get; set; }
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
