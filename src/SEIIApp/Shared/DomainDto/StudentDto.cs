using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class StudentDto
    {
        [Required]
        public string StudentName { get; set; }
        
        public string Password { get; set; }

        // public List<Course> EnrolledCourses { get; set; }
    }
}