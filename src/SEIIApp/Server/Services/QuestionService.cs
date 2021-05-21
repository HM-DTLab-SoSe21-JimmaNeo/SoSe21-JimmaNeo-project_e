using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services
{
    public class QuestionService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public QuestionService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
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

        public Question AddQuestion(Question newQuestion)
        {
            DatabaseContext.Questions.Add(newQuestion);
            DatabaseContext.SaveChanges();
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

        public Question AddQuestionToUser(Question toAddQuestion, Student toAddStudent, int questionStatus)
        {
            var userHistoryQuestion = new Question();

            Mapper.Map( toAddQuestion,userHistoryQuestion);
            userHistoryQuestion.QuestionStatus = questionStatus;
            userHistoryQuestion.QuestionId = 0;
            
            
            toAddStudent.WorkingQuestions.Add(userHistoryQuestion);

            DatabaseContext.Students.Update(toAddStudent);
            DatabaseContext.SaveChanges();

            return toAddQuestion;
        }
    }
}