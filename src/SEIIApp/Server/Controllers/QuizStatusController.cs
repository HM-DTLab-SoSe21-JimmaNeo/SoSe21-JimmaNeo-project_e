using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;
using SEIIApp.Server.Services.StatusServices;
using SEIIApp.Shared.DomainDto.StatusDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/quizstatus")]
    public class QuizStatusController : ControllerBase
    {
        private QuizStatusService QuizStatusService { get; set; }

        private QuizService QuizService { get; set; }

        private IMapper Mapper { get; set; }

        private UserService UserService { get; set; }

        public QuizStatusController(QuizStatusService quizStatusService, IMapper mapper, UserService userService,
            QuizService quizService)
        {
            this.QuizStatusService = quizStatusService;
            this.Mapper = mapper;
            this.UserService = userService;
            this.QuizService = quizService;
        }

        /// <summary>
        /// Add or update a quiz status for a quiz and a student. finished (true/false) required.
        /// </summary>
        /// <param name="quizId"></param>
        /// <param name="studentId"></param>
        /// <param name="finished"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<QuizStatusDto> AddOrUpdateQuizStatus([FromQuery] int quizId, [FromQuery] int studentId,
            [FromQuery] bool finished)
        {
            var quiz = QuizService.GetQuizById(quizId);
            var student = UserService.GetStudentById(studentId);

            if (quiz == null || student == null) return StatusCode(StatusCodes.Status404NotFound);


            var result = QuizStatusService.AddOrUpdateQuizStatus(quiz, student, finished);

            if (result == null) return StatusCode(StatusCodes.Status404NotFound);


            return Ok(result);
        }

        /// <summary>
        /// Get Quiz Status by Id
        /// </summary>
        /// <param name="quizStatusId"></param>
        /// <returns></returns>
        [HttpGet("{quizStatusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuizStatusDto> GetQuizStatusById(int quizStatusId)
        {
            var status = QuizStatusService.GetQuizStatusById(quizStatusId);
            if (status == null) return StatusCode(StatusCodes.Status404NotFound);

            return Mapper.Map<QuizStatusDto>(status);
        }
    }
}