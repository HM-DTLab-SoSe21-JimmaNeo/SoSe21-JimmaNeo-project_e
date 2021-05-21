using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class StudentDto
    {
        [Required]
        public string StudentName { get; set; }
        
        public string Password { get; set; }

        public CourseDto[] EnrolledCourses { get; set; }
        
        public int UserId { get; set; }
        
        public QuestionStatusDto[] QuestionStatusList { get; set; }
    }
}