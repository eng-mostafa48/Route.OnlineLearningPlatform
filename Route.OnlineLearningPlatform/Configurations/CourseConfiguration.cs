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
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .HasMaxLength(2000);

            builder.Property(c => c.Price)
                .HasPrecision(10, 2);

            builder.Property(c => c.Level)
                .IsRequired();

            builder.Property(c => c.IsPublished)
                .IsRequired();

            builder.Property(c => c.PublishedAt)
                .IsRequired(false);

            // Published Date Constraint
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Course_PublishedAt",
                "(IsPublished = 0 AND PublishedAt IS NULL) OR " +
                "(IsPublished = 1 AND PublishedAt IS NOT NULL)"
            ));
        }
    }
}
