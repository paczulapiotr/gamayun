using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gamayun.Infrastucture.Entities
{
    public class StudentSection
    {
        public int SectionID { get; set; }
        public Section Section { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        
        [Range(2,5)]
        public int Grade { get; set; }
    }

    public class StudentSectionEntityTypeConfiguration : IEntityTypeConfiguration<StudentSection>
    {

        public void Configure(EntityTypeBuilder<StudentSection> builder)
        {
            builder.HasKey(ss => new { ss.StudentID, ss.SectionID });
            
            builder.HasOne(s => s.Student)
                .WithMany(s => s.StudentSections)
                .HasForeignKey(a => a.StudentID);
            
            builder.HasOne(s => s.Section)
                .WithMany(s => s.StudentSections)
                .HasForeignKey(a => a.SectionID);
        }
    }
}
