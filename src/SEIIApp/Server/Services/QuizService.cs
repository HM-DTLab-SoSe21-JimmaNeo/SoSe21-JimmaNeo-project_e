using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using System.Linq;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;


namespace SEIIApp.Server.Services
{
    public class QuizService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }
        
        private ChapterService ChapterService { get; set; }

        public QuizService(DatabaseContext db, IMapper m, ChapterService chapterService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.ChapterService = chapterService;
        }

        private IQueryable<Quiz> GetQueryableForQuiz()
        {
            return DatabaseContext
                .Quiz
                .Include(quiz => quiz.Questions)
                .ThenInclude(question => question.Answers);
        }



        public Quiz[] GetAllQuizzes()
        {
            return GetQueryableForQuiz().ToArray();
        }


        public Quiz GetQuizById(int id)
        {
            return GetQueryableForQuiz().FirstOrDefault(x => x.QuizId == id);
        }

        public Quiz AddQuiz(Quiz newQuiz, Chapter modifiedChapter)
        {
            modifiedChapter.ChapterQuiz = newQuiz;
            ChapterService.UpdateChapter(modifiedChapter);
            return newQuiz;
        }

        public Quiz UpdateQuiz(Quiz updatedQuiz)
        {
            var existingQuiz = GetQuizById(updatedQuiz.QuizId);

            Mapper.Map(updatedQuiz, existingQuiz);
            DatabaseContext.Quiz.Update(existingQuiz);
            DatabaseContext.SaveChanges();
            return existingQuiz;
        }
        
        public void RemoveQuiz(Quiz removableQuiz)
        {
            DatabaseContext.Quiz.Remove(removableQuiz);
            DatabaseContext.SaveChanges();
        }
    }
}