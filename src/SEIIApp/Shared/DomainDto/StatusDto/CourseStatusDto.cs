using System;

namespace SEIIApp.Shared.DomainDto.StatusDto
{
    public class CourseStatusDto
    {
        public CourseDto Course { get; set; }

        public int FinishStatus { get; set; }
        
        public DateTime LastWorkedOn { get; set; }
    }
}