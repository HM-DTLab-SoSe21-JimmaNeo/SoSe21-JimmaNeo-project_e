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

        private IQueryable<Course> GetQueryableForCourse()
        {
            return DatabaseContext
                .Courses
                .Include(x => x.Chapters)
                .ThenInclude(x => x.ChapterQuiz)
                ;
        }

        private IQueryable<Chapter> GetQueryableForChapter()
        {
            return DatabaseContext.Chapters;
        }

        private IQueryable<Content> GetQueryableForContent()
        {
            return DatabaseContext.Contents;
        }

        public Course AddCourse(Course newCourse)
        {
            DatabaseContext.Courses.Add(newCourse);
            DatabaseContext.SaveChanges();
            return newCourse;
        }

        public Chapter AddChapter(Chapter newChapter)
        {
            DatabaseContext.Chapters.Add(newChapter);
            DatabaseContext.SaveChanges();
            return newChapter;
        }

        public Content AddContent(Content newContent)
        {
            DatabaseContext.Contents.Add(newContent);
            DatabaseContext.SaveChanges();
            return newContent;
        }

        public Course GetCourseById(int id)
        {
            return GetQueryableForCourse().FirstOrDefault(x => x.CourseId == id);
        }

        public Chapter GetChapterById(int id)
        {
            return GetQueryableForChapter().FirstOrDefault(x => x.ChapterId == id);
        }

        public Content GetContentById(int id)
        {
            return GetQueryableForContent().FirstOrDefault(x => x.ContentId == id);
        }
    }
}