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

        public QuizService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }

        private IQueryable<Quiz> GetQueryableForQuiz()
        {
            return DatabaseContext
                .Quiz
                .Include(quiz => quiz.Questions)
                .ThenInclude(question => question.Answers);
        }

        private IQueryable<Answer> GetQueryableForAnswer()
        {
            return DatabaseContext
                .Answers;
        }

        

        public Quiz[] GetAllQuizzes()
        {
            return GetQueryableForQuiz().ToArray();
        }

        public Answer[] GetAllAnswers()
        {
            return GetQueryableForAnswer().ToArray();
        }
        

        public Quiz GetQuizById(int id)
        {
            return GetQueryableForQuiz().FirstOrDefault(x => x.QuizId == id);
        }

        public Quiz AddQuiz(Quiz newQuiz)
        {
            DatabaseContext.Quiz.Add(newQuiz);
            DatabaseContext.SaveChanges();
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