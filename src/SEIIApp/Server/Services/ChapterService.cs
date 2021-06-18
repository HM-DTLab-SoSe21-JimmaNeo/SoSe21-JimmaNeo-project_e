using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Services
{
    public class ChapterService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public ChapterService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }
        
        private IQueryable<Chapter> GetQueryableForChapter()
        {
            return DatabaseContext
                .Chapters
                //.Include(x => x.ChapterName)
                .Include(x => x.ChapterQuiz)
                .Include(x => x.ChapterContentPdf)
                .Include(x => x.ChapterContentVideo)
                //.ThenInclude(y => y.ContentName)
                ;
        }

        public Chapter GetChapterById(int id)
        {
            return GetQueryableForChapter().FirstOrDefault(x => x.ChapterId == id);
        }

        public void RemoveChapter(Chapter toRemove)
        {
            DatabaseContext.Chapters.Remove(toRemove);
            DatabaseContext.SaveChanges();
        }

        public Chapter UpdateChapter(Chapter newChapter)
        {
            var existingChapter = GetChapterById(newChapter.ChapterId);

            Mapper.Map(newChapter, existingChapter);

            DatabaseContext.Chapters.Update(existingChapter);
            DatabaseContext.SaveChanges();
            return existingChapter;

        }
        
        public Chapter[] GetAllChapters()
        {
            return GetQueryableForChapter().ToArray();
        }

        public Chapter GetChapterByQuizId(int quizId)
        {
            var chapters = GetQueryableForChapter().ToList();
            return (from chapter in chapters where chapter.ChapterQuiz.QuizId == quizId select chapter).FirstOrDefault();
        }
    }
}