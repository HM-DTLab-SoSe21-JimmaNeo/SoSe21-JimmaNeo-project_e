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

        
    }
}