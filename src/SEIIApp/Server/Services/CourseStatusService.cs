using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services
{
    public class CourseStatusService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        private UserService UserService { get; set; }

        public CourseStatusService(DatabaseContext db, IMapper m, UserService userService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.UserService = userService;
        }

        private IQueryable<CourseStatus> GetQueryableForCourseStatus()
        {
            return DatabaseContext
                .CourseStatus
                .Include(x => x.Course)
                .Include(x => x.ChapterStatusList);
        }

        public CourseStatus GetCourseStatusById(int id)
        {
            return GetQueryableForCourseStatus().FirstOrDefault(x => x.CourseStatusId == id);
        }

        public CourseStatus[] GetAllEnrolledCoursesById(int id)
        {
            var user = UserService.GetStudentById(id);
            return user.EnrolledCourses.ToArray();
        }

        public CourseStatus AddCourseStatus(Course course, Student student)
        {
            var chapterStatusList = course.Chapters.Select(x =>
                new ChapterStatus()
                {
                    Chapter = x,
                    Finished = false
                }).ToList();

            var newStatus = new CourseStatus()
                {Course = course, ChapterStatusList = chapterStatusList, FinishStatus = 0};
            
            student.EnrolledCourses.Add(newStatus);

            

            DatabaseContext.Users.Update(student);
            DatabaseContext.CourseStatus.Add(newStatus);
            DatabaseContext.SaveChanges();

            return newStatus;
        }
    }
}