using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : ControllerBase
    {
        private QuizService QuizService { get; set; }
        private IMapper Mapper { get; set; }

        public QuizController(QuizService quizService, IMapper mapper)
        {
            this.Mapper = mapper;
            this.QuizService = quizService;
        }

        
    }
}