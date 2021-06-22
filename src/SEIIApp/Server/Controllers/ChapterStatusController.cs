using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Services;
using SEIIApp.Server.Services.StatusServices;
using SEIIApp.Shared.DomainDto;
using SEIIApp.Shared.DomainDto.StatusDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/chapterstatus")]
    public class ChapterStatusController : ControllerBase
    {
        private CourseStatusService CourseStatusService { get; set; }

        private CourseService CourseService { get; set; }

        private UserService UserService { get; set; }

        private ChapterService ChapterService { get; set; }

        private ChapterStatusService ChapterStatusService { get; set; }

        private IMapper Mapper { get; set; }

        public ChapterStatusController(CourseStatusService courseStatusService, IMapper mapper,
            CourseService courseService, UserService userService, ChapterStatusService chapterStatusService,
            ChapterService chapterService)
        {
            this.Mapper = mapper;
            this.CourseStatusService = courseStatusService;
            this.CourseService = courseService;
            this.UserService = userService;
            this.ChapterStatusService = chapterStatusService;
            this.ChapterService = chapterService;
        }

        /// <summary>
        /// Get a ChapterStatus by chapterStatusId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ChapterStatusDto> GetChapterStatusById([FromRoute] int id)
        {
            var status = ChapterStatusService.GetChapterStatusById(id);
            if (status == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<ChapterStatusDto>(status);

            return Ok(mapped);
        }

        /// <summary>
        /// Add or update a chapterStatus for a given chapter and user
        /// </summary>
        /// <param name="chapterStatusTransfer"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ChapterStatusDto> AddOrUpdateChapterStatus(
            [FromBody] chapterStatusTransfer chapterStatusTransfer)
        {
            var chapter = ChapterService.GetChapterById(chapterStatusTransfer.ChapterId);
            var student = UserService.GetStudentById(chapterStatusTransfer.UserId);

            if (chapter == null || student == null) return StatusCode(StatusCodes.Status404NotFound);

            var result = ChapterStatusService.AddOrUpdateChapterStatus(chapter, student);

            if (result == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<ChapterStatusDto>(result);


            return Ok(mapped);
        }

        /// <summary>
        /// Get the Chapter Status for the last chapter a student worked on 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getlast/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ChapterStatusDto> GetLastChapterStatusWorkedOn([FromRoute] int id)
        {
            var student = UserService.GetStudentById(id);
            var chapterStatus = ChapterStatusService.GetLastChapterStatusWorkedOn(student);
            return Ok(Mapper.Map<ChapterStatusDto>(chapterStatus));
        }
        
        
        /// <summary>
        /// Get all ChapterStatus for a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("all/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ChapterStatusDto[]> GetAllChapterStatusOfUser([FromRoute] int id)
        {
            var student = UserService.GetStudentById(id);
            var chapterStatuslist = ChapterStatusService.GetAllChapterStatusForUser(student);
            if (chapterStatuslist == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<ChapterStatusDto[]>(chapterStatuslist);
            return Ok(mapped);
        }
    }
}