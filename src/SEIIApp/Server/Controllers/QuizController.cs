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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuizDto> GetQuiz([FromRoute] int id)
        {
            var quiz = QuizService.GetQuizById(id);
            if (quiz == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedQuiz = Mapper.Map<QuizDto>(quiz);
            return Ok(mappedQuiz);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuestionDto[]> GetAllQuestions()
        {
            return Ok(Mapper.Map<QuestionDto[]>(QuizService.GetAllQuestions()));
        }
        
    }
}