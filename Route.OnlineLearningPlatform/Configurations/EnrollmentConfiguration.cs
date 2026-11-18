using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.OnlineLearningPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.OnlineLearningPlatform.Configurations
{
    internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            
            builder.HasKey(e => new
            {
                e.StudentId,
                e.CourseId
            });

            builder.Property(e => e.EnrollmentDate)
                .IsRequired();

            builder.Property(e => e.Progress)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired();

            builder.Property(e => e.FinalGrade)
                .HasPrecision(5, 2)
                .IsRequired(false);

            
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Enrollment_Progress",
                "Progress >= 0 AND Progress <= 100"
            ));

           
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Enrollment_Completed",
                "(Status <> 1 OR Progress = 100) AND " +
                "(Progress <> 100 OR Status = 1)"
            ));

            
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Enrollment_FinalGrade",
                "(FinalGrade IS NULL OR Status = 1)"
            ));

           
            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
