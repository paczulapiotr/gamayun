using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gamayun.Infrastucture.Entities
{
    public class PresenceDate : Entity
    {
        public DateTime Date { get; set; }
        public IEnumerable<Presence> Presences{ get; set; }
        public int? SectionID { get; set; }
        [ForeignKey(nameof(SectionID))]
        public Section Section { get; set; }
    }

    public class PresenceDateEntityTypeConfiguration : IEntityTypeConfiguration<PresenceDate>
    {
        public void Configure(EntityTypeBuilder<PresenceDate> builder)
        {
            builder.HasOne(a => a.Section)
                .WithMany(a => a.PresenceDates)
                .HasForeignKey(a => a.SectionID);
        }
    }
}
