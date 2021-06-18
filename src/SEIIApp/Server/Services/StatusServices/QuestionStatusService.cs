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
    public class QuestionStatusService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        private UserService UserService { get; set; }

        public QuestionStatusService(DatabaseContext db, IMapper m, UserService userService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.UserService = userService;
        }

        private IQueryable<QuestionStatus> GetQueryableForQuestionStatus()
        {
            return DatabaseContext
                .QuestionStatus
                .Include(x => x.Question)
                .ThenInclude(x => x.Answers);
        }

        public QuestionStatus GetQuestionStatusById(int id)
        {
            return GetQueryableForQuestionStatus().FirstOrDefault(x => x.QuestionStatusId == id);
        }

        public QuestionStatus[] GetAllQuestionStatusOfUser(int userId)
        {
            var user = UserService.GetStudentById(userId);
            return user.QuestionStatusList.ToArray();
        }

        public QuestionStatus[] GetAllPendingQuestionStatusOfUser(int userId)
        {
            var user = UserService.GetStudentById(userId);
            var questionStatusEnumerableI = user.QuestionStatusList.Where(x =>
                x.QuestionLevel == 1 && DateTime.Now - x.LastAnswered >= TimeSpan.FromHours(24)); //zum testen!!
            var questionStatusEnumerableII = user.QuestionStatusList.Where(x =>
                x.QuestionLevel == 2 && DateTime.Now - x.LastAnswered >= TimeSpan.FromHours(48));
            var questionStatusEnumerableIII = user.QuestionStatusList.Where(x =>
                x.QuestionLevel == 3 && DateTime.Now - x.LastAnswered >= TimeSpan.FromDays(4));
            var questionStatusEnumerableIV = user.QuestionStatusList.Where(x =>
                x.QuestionLevel == 4 && DateTime.Now - x.LastAnswered >= TimeSpan.FromDays(6));
            var questionStatusEnumerableV = user.QuestionStatusList.Where(x =>
                x.QuestionLevel == 5 && x.LastAnswered - DateTime.Now >= TimeSpan.FromDays(21));
            var combined = questionStatusEnumerableI.Concat(questionStatusEnumerableII)
                .Concat(questionStatusEnumerableIII).Concat(questionStatusEnumerableIV);

            return combined.ToArray();
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


            DatabaseContext.Users.Update(student);
            DatabaseContext.QuestionStatus.Update(searchStatus);
            DatabaseContext.SaveChanges();

            return searchStatus;
        }
        
        public QuestionStatus AddOrUpdateQuestionStatus(Question question, Student student, int questionStatus, DateTime time)
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

            searchStatus.LastAnswered = time;


            DatabaseContext.Users.Update(student);
            DatabaseContext.QuestionStatus.Update(searchStatus);
            DatabaseContext.SaveChanges();

            return searchStatus;
        }
    }
}