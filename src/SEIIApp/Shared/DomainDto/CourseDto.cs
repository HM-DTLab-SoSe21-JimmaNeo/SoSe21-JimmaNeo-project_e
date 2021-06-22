using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class CourseDto
    {
        public string CourseName { get; set; }

        public ChapterDto[] Chapters { get; set; }
        
        public int CourseId { get; set; }
    }
}