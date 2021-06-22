using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class Course
    {
        [Key] public int CourseId { get; set; }

        public string CourseName { get; set; }

        public List<Chapter> Chapters { get; set; }
    }
}