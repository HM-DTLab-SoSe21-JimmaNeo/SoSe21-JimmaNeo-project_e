using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services
{
    public class QuestionService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }
        
        private QuizService QuizService { get; set; }

        public QuestionService(DatabaseContext db, IMapper m, QuizService quizService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.QuizService = quizService;
        }

        private IQueryable<Question> GetQueryableForQuestions()
        {
            return DatabaseContext
                .Questions
                .Include(question => question.Answers);
        }

        public Question[] GetAllQuestions()
        {
            return GetQueryableForQuestions().ToArray();
        }

        public Question GetQuestionById(int id)
        {
            return GetQueryableForQuestions().FirstOrDefault(x => x.QuestionId == id);
        }

        public Question AddQuestion(Question newQuestion, Quiz modifiedQuiz)
        {
            modifiedQuiz.Questions.Add(newQuestion);
            QuizService.UpdateQuiz(modifiedQuiz);
            return newQuestion;
        }

        public Question UpdateQuestion(Question updatedQuestion)
        {
            var existingQuestion = GetQuestionById(updatedQuestion.QuestionId);
            Mapper.Map(updatedQuestion, existingQuestion);
            DatabaseContext.Questions.Update(existingQuestion);
            DatabaseContext.SaveChanges();
            return existingQuestion;
        }

        public void RemoveQuestion(Question removableQuestion)
        {
            DatabaseContext.Questions.Remove(removableQuestion);
            DatabaseContext.SaveChanges();
        }
    }
}