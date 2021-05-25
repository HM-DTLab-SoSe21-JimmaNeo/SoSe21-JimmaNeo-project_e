using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;

namespace SEIIApp.Server.Domain.UserDomain
{
    public class Student : User
    {
        public List<CourseStatus> EnrolledCourses { get; set; }

        public List<QuestionStatus> QuestionStatusList { get; set; }
        
        
    }
}