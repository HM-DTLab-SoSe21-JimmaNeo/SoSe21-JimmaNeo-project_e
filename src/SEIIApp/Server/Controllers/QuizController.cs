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
        
        /// <summary>
        /// Get a Quiz by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuizDto> GetQuizById([FromRoute] int id)
        {
            var result = QuizService.GetQuizById(id);
            if (result == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<QuizDto>(result);
            return Ok(mapped);
        }

        
    }
}