using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/question")]
    public class QuestionController : ControllerBase
    {
        private QuestionService QuestionService { get; set; }
        
        private UserService UserService { get; set; }
        private IMapper Mapper { get; set; }

        public QuestionController(QuestionService questionService, IMapper mapper, UserService userService)
        {
            this.QuestionService = questionService;
            this.Mapper = mapper;
            this.UserService = userService;
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<QuestionDto> AddQuestionToUser([FromQuery] int toAddQuestionId,[FromQuery] int toAddStudentId,[FromQuery] int questionStatus  )
        {
            var toAddQuestion = QuestionService.GetQuestionById(toAddQuestionId);
            var toAddStudent = UserService.GetStudentById(toAddStudentId);

            QuestionService.AddQuestionToUser(toAddQuestion, toAddStudent, questionStatus);

            return Ok(toAddQuestion);

        }
    }
    
    
}