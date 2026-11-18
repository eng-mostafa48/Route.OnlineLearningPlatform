using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.OnlineLearningPlatform.Models
{
    internal class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        // Parent Category
        public Category? ParentCategory { get; set; }

        // Child Categories
        public ICollection<Category> SubCategories { get; set; }
            = new List<Category>();

        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
