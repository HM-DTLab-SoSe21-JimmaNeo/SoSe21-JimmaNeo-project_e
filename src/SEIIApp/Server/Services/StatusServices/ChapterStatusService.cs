using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services.StatusServices
{
    public class ChapterStatusService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        private UserService UserService { get; set; }

        public ChapterStatusService(DatabaseContext db, IMapper m, UserService userService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.UserService = userService;
        }

        private IQueryable<ChapterStatus> GetQueryableForChapterStatus()
        {
            return DatabaseContext
                .ChapterStatus
                .Include(x => x.Chapter);
        }

        public ChapterStatus GetChapterStatusById(int id)
        {
            return GetQueryableForChapterStatus().FirstOrDefault(x => x.ChapterStatusId == id);
        }

        public ChapterStatus AddOrUpdateChapterStatus(Chapter chapter, Student student)
        {
            var result = student.ChapterStatuslist.Find(x => x.Chapter.ChapterId == chapter.ChapterId);

            if (result == null)
            {
                result = new ChapterStatus() {Chapter = chapter, Finished = false};
            }
            else
            {
                var chapterQuiz = chapter.ChapterQuiz;
                var chapterQuizStatus = student.QuizStatusList.Find(x => x.Quiz.QuizId == chapterQuiz.QuizId);
                
                result.Finished = chapterQuizStatus != null && chapterQuizStatus.Finished;
            }
            
            result.LastWorkedOn = DateTime.Now;

            DatabaseContext.Users.Update(student);
            DatabaseContext.ChapterStatus.Update(result);
            DatabaseContext.SaveChanges();

            return result;
        }


    }
}