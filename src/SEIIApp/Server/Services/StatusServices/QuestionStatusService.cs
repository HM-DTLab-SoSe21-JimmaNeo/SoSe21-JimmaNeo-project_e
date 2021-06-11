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
            
            var result = user.QuestionStatusList.Where(x =>
                (x.QuestionLevel == 1 && DateTime.Now - x.LastAnswered >= TimeSpan.FromHours(24))
                || (x.QuestionLevel == 2 && DateTime.Now - x.LastAnswered >= TimeSpan.FromHours(48))
                || (x.QuestionLevel == 3 && DateTime.Now - x.LastAnswered >= TimeSpan.FromDays(4))
                || (x.QuestionLevel == 4 && DateTime.Now - x.LastAnswered >= TimeSpan.FromDays(6))
                || (x.QuestionLevel == 5 && x.LastAnswered - DateTime.Now >= TimeSpan.FromDays(21)));

            return result.ToArray();
        }

        public QuestionStatus AddOrUpdateQuestionStatus(Question question, Student student, int questionStatus)
        {
            return AddOrUpdateQuestionStatus(question, student, questionStatus, DateTime.Now);
        }

        public QuestionStatus AddOrUpdateQuestionStatus(Question question, Student student, int questionStatus,
            DateTime time)
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