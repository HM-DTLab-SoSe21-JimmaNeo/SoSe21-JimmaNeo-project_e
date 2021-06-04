using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus
{
    public class CourseStatus
    {
        [Key] public int CourseStatusId { get; set; }
        public Course Course { get; set; }

        public int FinishStatus { get; set; }
        
        public DateTime LastWorkedOn { get; set; }
        
        //public List<ChapterStatus> ChapterStatusList { get; set; }
    }
}