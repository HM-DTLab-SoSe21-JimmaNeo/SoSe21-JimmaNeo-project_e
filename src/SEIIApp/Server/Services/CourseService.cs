using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Services
{
    public class CourseService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public CourseService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }

        /// <summary>
        /// Database Query for Courses
        /// </summary>
        /// <returns></returns>
        private IQueryable<Course> GetQueryableForCourse()
        {
            return DatabaseContext
                    .Courses
                    .Include(x => x.Chapters)
                    .ThenInclude(x => x.ChapterQuiz)
                    .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.Answers)
                    .Include(x => x.Chapters)
                    .ThenInclude(x => x.ChapterContentPdf)
                ;
        }

        /// <summary>
        /// Return Course By given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Course GetCourseById(int id)
        {
            return GetQueryableForCourse().FirstOrDefault(x => x.CourseId == id);
        }

        /// <summary>
        /// Add a new course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Course AddCourse(Course course)
        {
            DatabaseContext.Courses.Add(course);
            DatabaseContext.SaveChanges();
            return course;
        }

        /// <summary>
        /// Update a given course
        /// </summary>
        /// <param name="newCourse"></param>
        /// <returns></returns>
        public Course UpdateCourse(Course newCourse)
        {
            var existingCourse = GetCourseById(newCourse.CourseId);

            Mapper.Map(newCourse, existingCourse);
            DatabaseContext.Courses.Update(existingCourse);
            DatabaseContext.SaveChanges();
            return existingCourse;
        }

        /// <summary>
        /// Removes a given course from database
        /// </summary>
        /// <param name="course"></param>
        public void RemoveCourse(Course course)
        {
            DatabaseContext.Courses.Remove(course);
            DatabaseContext.SaveChanges();
        }

        /// <summary>
        /// Returns Array of all Courses in the database
        /// </summary>
        /// <returns></returns>
        public Course[] GetAllCourses()
        {
            return GetQueryableForCourse().ToArray();
        }
    }
}