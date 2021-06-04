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
    public class QuizStatusService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        private UserService UserService { get; set; }

        public QuizStatusService(DatabaseContext db, IMapper m, UserService userService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.UserService = userService;
        }

        private IQueryable<QuizStatus> GetQueryableForQuizStatus()
        {
            return DatabaseContext
                .QuizStatus
                .Include(x => x.Quiz);
        }

        public QuizStatus GetQuizStatusById(int id)
        {
            return GetQueryableForQuizStatus().FirstOrDefault(x => x.QuizStatusId == id);
        }

        public QuizStatus AddOrUpdateQuizStatus(Quiz quiz, Student student, bool finished)
        {
            var searchStatus = student.QuizStatusList.Find(x => x.Quiz.QuizId == quiz.QuizId);

            if (searchStatus == null)
            {
                searchStatus = new QuizStatus() {Quiz = quiz, Finished = finished, LastAnswered = DateTime.Now};
                student.QuizStatusList.Add(searchStatus);
            }
            else
            {
                searchStatus.Finished = finished;
                searchStatus.LastAnswered = DateTime.Now;
            }

            DatabaseContext.Update(student);
            DatabaseContext.QuizStatus.Update(searchStatus);
            DatabaseContext.SaveChanges();

            return searchStatus;
        }

        
    }
}