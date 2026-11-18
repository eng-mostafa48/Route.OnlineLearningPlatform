using Microsoft.EntityFrameworkCore;
using Route.OnlineLearningPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.OnlineLearningPlatform.Context
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                 "Server=.;Database=OnlineLearningDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );

            
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@example.com",
                    Bio = "Backend Developer",
                    JoinedAt = new DateTime(2025, 1, 10)
                },
                new Instructor
                {
                    Id = 2,
                    FullName = "Sara Ali",
                    Email = "sara@example.com",
                    Bio = "Software Engineer",
                    JoinedAt = new DateTime(2025, 2, 15)
                }
            );

          
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Programming",
                    Description = "Programming courses",
                    ParentCategoryId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Backend",
                    Description = "Backend development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "ASP.NET",
                    Description = "ASP.NET development",
                    ParentCategoryId = 2
                }
            );

       
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "C# Fundamentals",
                    Description = "Learn C# from scratch",
                    Price = 500,
                    Level = Enums.CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 3, 1),
                    InstructorId = 1,
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET Core",
                    Description = "Learn ASP.NET Core",
                    Price = 1000,
                    Level = Enums.CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 1),
                    InstructorId = 2,
                    CategoryId = 3
                },
                new Course
                {
                    Id = 3,
                    Title = "Advanced C#",
                    Description = "Advanced C# concepts",
                    Price = 750,
                    Level = Enums.CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 1,
                    CategoryId = 2
                }
            );

        
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FullName = "Omar Hassan",
                    Email = "omar@example.com",
                    DateOfBirth = new DateTime(2003, 5, 10)
                },
                new Student
                {
                    Id = 2,
                    FullName = "Mona Ahmed",
                    Email = "mona@example.com",
                    DateOfBirth = new DateTime(2002, 8, 20)
                }
            );

           
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 1,
                    EnrollmentDate = new DateTime(2025, 5, 1),
                    Progress = 100,
                    Status = Enums.EnrollmentStatus.Completed,
                    FinalGrade = 95
                },
                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 2,
                    EnrollmentDate = new DateTime(2025, 5, 5),
                    Progress = 45,
                    Status = Enums.EnrollmentStatus.Active,
                    FinalGrade = null
                }
            );

        }
    }
}
