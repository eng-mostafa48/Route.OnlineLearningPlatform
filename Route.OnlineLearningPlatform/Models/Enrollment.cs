using Route.OnlineLearningPlatform.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.OnlineLearningPlatform.Models
{
    internal class Enrollment
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public decimal Progress { get; set; }

        public EnrollmentStatus Status { get; set; }

        public decimal? FinalGrade { get; set; }

        // Navigation Properties
        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}
