using System.Collections.Generic;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Domain.UserDomain
{
    public class Instructor : User
    {
        public List<Course> ManagedCourses { get; set; }

    }
}
