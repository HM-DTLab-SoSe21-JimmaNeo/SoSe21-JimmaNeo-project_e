using System.Collections.Generic;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Domain.UserDomain
{
    public class Student : User
    {
        public string StudentName { get; set; }
        
        public string Password { get; set; }
        
        //public List<Course> EnrolledCourses { get; set; }

    }
}
