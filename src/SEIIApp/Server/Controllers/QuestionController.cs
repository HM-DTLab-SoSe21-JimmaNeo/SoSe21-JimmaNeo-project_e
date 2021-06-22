using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;
using SEIIApp.Shared.DomainDto.StatusDto;

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
        
        /// <summary>
        /// Get a Question  by Question Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Question> GetQuestionById([FromRoute] int id)
        {
            var status = QuestionService.GetQuestionById(id);
            if(status == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<QuestionDto>(status);
            return Ok(mapped);
        }
        

    }
    
    
}