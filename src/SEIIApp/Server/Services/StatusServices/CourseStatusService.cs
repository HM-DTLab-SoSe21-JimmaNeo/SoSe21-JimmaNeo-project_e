using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services.StatusServices
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
                    .ThenInclude(y => y.Chapters)
                ;
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

        public CourseStatus AddOrUpdateCourseStatus(Course course, Student student)
        {
            var searchStatus = student.EnrolledCourses.Find(x => x.Course.CourseId == course.CourseId);

            if (searchStatus == null)
            {
                searchStatus = new CourseStatus() {Course = course, FinishStatus = 0};
                student.EnrolledCourses.Add(searchStatus);
            }
            else
            {
                var foundChapters = (from chapter in student.ChapterStatuslist
                    where chapter.Finished
                    where course.Chapters.Contains(chapter.Chapter)
                    select chapter).ToList();

                searchStatus.FinishStatus = (float) foundChapters.Count / course.Chapters.Count;
            }

            searchStatus.LastWorkedOn = DateTime.Now;

            DatabaseContext.Users.Update(student);
            DatabaseContext.CourseStatus.Update(searchStatus);
            DatabaseContext.SaveChanges();

            return searchStatus;
        }

        public CourseStatus GetLastCourseStatusWorkedOn(Student student)
        {
            if (student.EnrolledCourses is {Count: > 0})
            {
                var search = student.EnrolledCourses.Aggregate((i1, i2) => i1.LastWorkedOn > i2.LastWorkedOn ? i1 : i2);
                return search;
            }
            else
            {
                return null;
            }
        }
    }
}