using System;
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


        public QuestionStatus AddOrUpdateQuestionStatus(Question question, Student student, int questionStatus)
        {
            var searchStatus = student.QuestionStatusList.Find(x => x.Question.QuestionId == question.QuestionId);

            if (searchStatus == null)
            {
                searchStatus = new QuestionStatus() {Question = question, QuestionLevel = questionStatus};
                student.QuestionStatusList.Add(searchStatus);
            }
            else
            {
                searchStatus.QuestionLevel = questionStatus;
            }
            
            searchStatus.LastAnswered = DateTime.Now;

            DatabaseContext.Students.Update(student);
            DatabaseContext.SaveChanges();

            return searchStatus;
        }
    }
}