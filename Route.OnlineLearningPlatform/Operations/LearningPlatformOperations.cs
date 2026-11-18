using Microsoft.EntityFrameworkCore;
using Route.OnlineLearningPlatform.Context;
using Route.OnlineLearningPlatform.Enums;
using Route.OnlineLearningPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.OnlineLearningPlatform.Operations
{
    internal class LearningPlatformOperations
    {
        public static void AddInstructor(ApplicationDbContext context)
        {
            var instructor = new Instructor
            {
                FullName = "Mohamed Hassan",
                Email = "mohamed@example.com",
                Bio = "C# and Backend Developer",
                JoinedAt = DateTime.Now
            };

            try
            {
                context.Instructors.Add(instructor);
                context.SaveChanges();

                Console.WriteLine(
                    $"Instructor added successfully. ID: {instructor.Id}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not add instructor: {ex.GetBaseException().Message}");
            }
        }

        public static void AddCourse(
                       ApplicationDbContext context,
                       int instructorId,
                       int categoryId)
        {
            var instructor = context.Instructors.Find(instructorId);

            if (instructor == null)
            {
                Console.WriteLine("Instructor not found. Course was not added.");
                return;
            }

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found. Course was not added.");
                return;
            }

            var course = new Course
            {
                Title = "Entity Framework Core",
                Description = "Learn EF Core from scratch.",
                Price = 800,
                Level = CourseLevel.Intermediate,
                IsPublished = false,
                PublishedAt = null,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            try
            {
                context.Courses.Add(course);
                context.SaveChanges();

                Console.WriteLine(
                    $"Course added successfully. ID: {course.Id}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not add course: {ex.GetBaseException().Message}");
            }
        }

        public static void AddStudents(ApplicationDbContext context)
        {
            var students = new List<Student>
            {
                new Student
                {
                    FullName = "Ali Mohamed",
                    Email = "ali@example.com",
                    DateOfBirth = new DateTime(2003, 5, 10)
                },
            
                new Student
                {
                    FullName = "Sara Ahmed",
                    Email = "sara@example.com",
                    DateOfBirth = new DateTime(2002, 8, 15)
                },
            
                new Student
                {
                    FullName = "Omar Hassan",
                    Email = "omar2@example.com",
                    DateOfBirth = new DateTime(2004, 3, 20)
                }
            };

            try
            {
                context.Students.AddRange(students);

                context.SaveChanges();

                Console.WriteLine("Three students added successfully:");

                foreach (var student in students)
                {
                    Console.WriteLine(
                        $"{student.FullName} - ID: {student.Id}");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not add students: {ex.GetBaseException().Message}");
            }
        }

        public static void EnrollStudent(
                       ApplicationDbContext context,
                       int studentId,
                       int courseId)
        {
            var student = context.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Enrollment refused: Student not found.");
                return;
            }

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Enrollment refused: Course not found.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine(
                    "Enrollment refused: Course is not published.");
                return;
            }

            var existingEnrollment = context.Enrollments.Find(
                studentId, courseId);

            if (existingEnrollment != null)
            {
                Console.WriteLine(
                    "Enrollment refused: Student is already enrolled.");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active,
                FinalGrade = null
            };

            try
            {
                context.Enrollments.Add(enrollment);
                context.SaveChanges();

                Console.WriteLine("Student enrolled successfully.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Enrollment failed: {ex.GetBaseException().Message}");
            }
        }

        public static void FindCourseById(
                       ApplicationDbContext context,
                       int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.WriteLine(
                $"ID: {course.Id}, Title: {course.Title}, Price: {course.Price}");
        }

        public static void FindCourseByTitle(
                       ApplicationDbContext context,
                       string title)
        {
            var course = context.Courses.FirstOrDefault(
                c => c.Title == title);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.WriteLine(
                $"ID: {course.Id}, Title: {course.Title}, Price: {course.Price}");
        }

        public static void FilterCourses(
                       ApplicationDbContext context,
                       decimal minPrice,
                       decimal maxPrice)
        {
            var courses = context.Courses
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .Select(c => new
                {
                    c.Title,
                    c.Level,
                    c.Price
                })
                .ToList();

            if (courses.Count == 0)
            {
                Console.WriteLine("No courses found in this price range.");
                return;
            }

            Console.WriteLine("Filtered Courses:");

            foreach (var course in courses)
            {
                Console.WriteLine(
                    $"{course.Title} | {course.Level} | {course.Price:C}");
            }
        }

        public static void PrintCategories(
                       ApplicationDbContext context,
                       int categoryId)
        {
           
            var subCategories = context.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToList();

            Console.WriteLine("Sub-categories:");

            if (subCategories.Count == 0)
            {
                Console.WriteLine("No sub-categories found.");
            }
            else
            {
                foreach (var category in subCategories)
                {
                    Console.WriteLine(
                        $"{category.Id} - {category.Name}");
                }
            }

            
            var topLevelCategories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("\nTop-level categories:");

            if (topLevelCategories.Count == 0)
            {
                Console.WriteLine("No top-level categories found.");
            }
            else
            {
                foreach (var category in topLevelCategories)
                {
                    Console.WriteLine(
                        $"{category.Id} - {category.Name}");
                }
            }
        }

        public static void PrintCourseRoster(
                       ApplicationDbContext context,
                       int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            
            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine(
                $"Course: {course.Title}");

            Console.WriteLine(
                $"Number of enrolled students: {studentIds.Count}");

            if (studentIds.Count == 0)
            {
                Console.WriteLine("No students enrolled.");
                return;
            }

          
            var students = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            Console.WriteLine("Students:");

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.Id} - {student.FullName}");
            }
        }

        public static void ChangeCoursePrice(
                       ApplicationDbContext context,
                       int courseId,
                       decimal newPrice)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (newPrice < 0)
            {
                Console.WriteLine("Invalid price. Price cannot be negative.");
                return;
            }

            course.Price = newPrice;

            try
            {
                context.SaveChanges();

                Console.WriteLine(
                    $"Course price updated successfully. New price: {course.Price}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not update price: {ex.GetBaseException().Message}");
            }
        }

        public static void UpdateEnrollmentProgress(
                       ApplicationDbContext context,
                       int studentId,
                       int courseId,
                       decimal newProgress)
        {
            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine(
                    "Invalid progress. Progress must be between 0 and 100.");
                return;
            }

            var enrollment = context.Enrollments.Find(
                studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            enrollment.Progress = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }
            else
            {
                enrollment.Status = EnrollmentStatus.Active;
                enrollment.FinalGrade = null;
            }

            try
            {
                context.SaveChanges();

                Console.WriteLine("Enrollment progress updated successfully.");

                Console.WriteLine(
                    $"Progress: {enrollment.Progress}%");

                Console.WriteLine(
                    $"Status: {enrollment.Status}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not update enrollment: {ex.GetBaseException().Message}");
            }
        }

        public static void PublishCourse(
                       ApplicationDbContext context,
                       int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (course.IsPublished)
            {
                Console.WriteLine("Course is already published.");
                return;
            }

            course.IsPublished = true;
            course.PublishedAt = DateTime.Now;

            try
            {
                context.SaveChanges();

                Console.WriteLine(
                    $"Course '{course.Title}' published successfully.");

                Console.WriteLine(
                    $"Published At: {course.PublishedAt}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not publish course: {ex.GetBaseException().Message}");
            }
        }

        public static void UnenrollStudent(
                       ApplicationDbContext context,
                       int studentId,
                       int courseId)
        {
            var enrollment = context.Enrollments.Find(
                studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            try
            {
                context.Enrollments.Remove(enrollment);

                context.SaveChanges();

                Console.WriteLine("Student unenrolled successfully.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not unenroll student: {ex.GetBaseException().Message}");
            }
        }

        public static void TryDeleteCategory(
                       ApplicationDbContext context,
                       int categoryId)
        {
            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            try
            {
                context.Categories.Remove(category);

                context.SaveChanges();

                Console.WriteLine("Category deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Cannot delete this category.");

                Console.WriteLine(
                    $"Database message: {ex.GetBaseException().Message}");

                
                context.Entry(category).State = EntityState.Detached;
            }
        }

        public static void SafeDeleteCategory(
                       ApplicationDbContext context,
                       int categoryId)
        {
            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            
            bool hasSubCategories = context.Categories
                .Any(c => c.ParentCategoryId == categoryId);

            if (hasSubCategories)
            {
                Console.WriteLine(
                    "Cannot delete category because it has sub-categories.");

                return;
            }

            try
            {
                context.Categories.Remove(category);

                context.SaveChanges();

                Console.WriteLine("Category deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not delete category: {ex.GetBaseException().Message}");
            }
        }

        public static void DeleteCourse(
                       ApplicationDbContext context,
                       int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            
            int beforeCount = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments before deletion: {beforeCount}");

            try
            {
                context.Courses.Remove(course);

                context.SaveChanges();

                
                int afterCount = context.Enrollments
                    .Count(e => e.CourseId == courseId);

                Console.WriteLine(
                    $"Enrollments after deletion: {afterCount}");

                Console.WriteLine("Course deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not delete course: {ex.GetBaseException().Message}");
            }
        }

    }
}
