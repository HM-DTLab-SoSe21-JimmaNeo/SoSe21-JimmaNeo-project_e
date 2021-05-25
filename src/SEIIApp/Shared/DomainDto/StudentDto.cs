using System.ComponentModel.DataAnnotations;
using SEIIApp.Shared.DomainDto.StatusDto;

namespace SEIIApp.Shared.DomainDto
{
    public class StudentDto : UserDto
    {
        public CourseStatusDto[] EnrolledCourses { get; set; }


        public QuestionStatusDto[] QuestionStatusList { get; set; }
    }
}