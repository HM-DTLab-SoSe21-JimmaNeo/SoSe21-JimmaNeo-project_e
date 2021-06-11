using System;
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
    [Route("api/questionstatus")]
    public class QuestionStatusController : ControllerBase
    {
        private QuestionStatusService QuestionStatusService { get; set; }

        private UserService UserService { get; set; }

        private QuestionService QuestionService { get; set; }

        private IMapper Mapper { get; set; }

        public QuestionStatusController(QuestionStatusService questionStatusService, UserService userService,
            QuestionService questionService, IMapper mapper)
        {
            this.Mapper = mapper;
            this.UserService = userService;
            this.QuestionStatusService = questionStatusService;
            this.QuestionService = questionService;
        }

        /// <summary>
        /// Get a Question Status by Question Status Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuestionStatus> GetQuestionStatusById([FromRoute] int id)
        {
            var status = QuestionStatusService.GetQuestionStatusById(id);
            if (status == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<QuestionStatusDto>(status);
            return Ok(mapped);
        }

        /// <summary>
        /// Get All Questions due for repetition for a user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuestionStatus[]> GetAllPendingQuestionStatusOfUser([FromQuery] int userId)
        {
            var statusList = QuestionStatusService.GetAllPendingQuestionStatusOfUser(userId);
            //var statusList = QuestionStatusService.GetAllQuestionStatusOfUser(userId);
            if (statusList == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<QuestionStatusDto[]>(statusList);
            return Ok(mapped);
        }

        /// <summary>
        /// By giving Question (not questionstatus) id, userid(must be of student) and questionstatus add
        /// a new or update an existing question status fot the given question for the given user.
        /// </summary>
        /// <param name="toAddQuestionId"></param>
        /// <param name="toAddStudentId"></param>
        /// <param name="questionStatus"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<QuestionStatusDto> AddOrUpdateQuestionStatusForUser([FromQuery] int toAddQuestionId,
            [FromQuery] int toAddStudentId, [FromQuery] int questionStatus)
        {
            var toAddQuestion = QuestionService.GetQuestionById(toAddQuestionId);
            var toAddStudent = UserService.GetStudentById(toAddStudentId);

            if (toAddQuestion == null || toAddStudent == null) return StatusCode(StatusCodes.Status404NotFound);


            var result = QuestionStatusService.AddOrUpdateQuestionStatus(toAddQuestion, toAddStudent, questionStatus);

            if (result == null) return StatusCode(StatusCodes.Status404NotFound);


            return Ok(result);
        }

        /// <summary>
        /// Get all QuestionStatus for a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("~/allquestionstatus/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<QuestionStatusDto[]> GetAllQuestionStatusOfUser([FromRoute] int id)
        {
            var questionstatuslist = QuestionStatusService.GetAllQuestionStatusOfUser(id);
            if (questionstatuslist == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<QuestionStatusDto[]>(questionstatuslist);
            return Ok(mapped);
        }
    }
}