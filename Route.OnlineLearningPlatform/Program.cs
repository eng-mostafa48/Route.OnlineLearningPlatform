using Microsoft.Extensions.Options;
using Route.OnlineLearningPlatform.Context;
using Route.OnlineLearningPlatform.Operations;

namespace Route.OnlineLearningPlatform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new ApplicationDbContext();

            Console.WriteLine("========== CREATE OPERATIONS ==========\n");

            
            LearningPlatformOperations.AddInstructor(context);

            
            var newInstructor = context.Instructors
                .FirstOrDefault(i => i.Email == "mohamed@example.com");

            
            if (newInstructor != null)
            {
                LearningPlatformOperations.AddCourse(
                    context,
                    newInstructor.Id,
                    2);
            }

            
            LearningPlatformOperations.AddStudents(context);

            
            var newStudent = context.Students
                .FirstOrDefault(s => s.Email == "ali@example.com");


            Console.WriteLine("\n========== READ OPERATIONS ==========\n");

            
            LearningPlatformOperations.FindCourseById(context, 1);

            
            LearningPlatformOperations.FindCourseByTitle(
                context, "C# Fundamentals");

            
            LearningPlatformOperations.FilterCourses(
                context, 400, 1000);

            
            LearningPlatformOperations.PrintCategories(context, 1);

            LearningPlatformOperations.PrintCourseRoster(context, 1);


            Console.WriteLine("\n========== UPDATE OPERATIONS ==========\n");

            LearningPlatformOperations.ChangeCoursePrice(
                context, 2, 1200);

            LearningPlatformOperations.UpdateEnrollmentProgress(
                context, 2, 2, 65);

            LearningPlatformOperations.PublishCourse(context, 3);


            Console.WriteLine("\n========== ENROLLMENT OPERATIONS ==========\n");

            if (newStudent != null)
            {
                LearningPlatformOperations.EnrollStudent(
                    context, newStudent.Id, 1);
            }

            if (newStudent != null)
            {
                LearningPlatformOperations.UnenrollStudent(
                    context, newStudent.Id, 1);
            }


            Console.WriteLine("\n========== DELETE OPERATIONS ==========\n");

            LearningPlatformOperations.TryDeleteCategory(context, 1);

            LearningPlatformOperations.SafeDeleteCategory(context, 1);

            LearningPlatformOperations.DeleteCourse(context, 1);


            Console.WriteLine("\n========== OPERATIONS COMPLETED ==========");
        }
    }
}
