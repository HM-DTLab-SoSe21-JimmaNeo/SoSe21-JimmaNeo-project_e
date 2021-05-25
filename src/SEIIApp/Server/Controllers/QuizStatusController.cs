using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/quizstatus")]
    public class QuizStatusController
    {
        
        private QuizStatusService QuizStatusService { get; set; }
        
        private QuizService QuizService { get; set; }
        
        private IMapper Mapper { get; set; }
        
        private UserService UserService { get; set; }

        public QuizStatusController(QuizStatusService quizStatusService, IMapper mapper, UserService userService, QuizService quizService)
        {
            this.QuizStatusService = quizStatusService;
            this.Mapper = mapper;
            this.UserService = userService;
            this.QuizService = quizService;
        }

        
        
    }
}