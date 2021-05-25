using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto.StatusDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/coursestatus")]
    public class CourseStatusController : ControllerBase
    {
        private CourseStatusService CourseStatusService { get; set; }

        private CourseService CourseService { get; set; }

        private UserService UserService { get; set; }

        private IMapper Mapper { get; set; }

        public CourseStatusController(CourseStatusService courseStatusService, IMapper mapper,
            CourseService courseService, UserService userService)
        {
            this.Mapper = mapper;
            this.CourseStatusService = courseStatusService;
            this.CourseService = courseService;
            this.UserService = userService;
        }

        /// <summary>
        /// Adds a course status (= enroll a student in a course)
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CourseStatusDto> AddCourseStatus([FromQuery] int courseId,
            [FromQuery] int studentId)
        {
            var course = CourseService.GetCourseById(courseId);
            var student = UserService.GetStudentById(studentId);

            var result = CourseStatusService.AddCourseStatus(course, student);

            return Ok(result);
        }

        /// <summary>
        /// Get Course Status by Id
        /// </summary>
        /// <param name="courseStatusId"></param>
        /// <returns></returns>
        [HttpGet("{courseStatusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseStatusDto> GetCourseStatusById([FromRoute] int courseStatusId)
        {
            var status = CourseStatusService.GetCourseStatusById(courseStatusId);
            if (status == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<CourseStatusDto>(status);
            return Ok(mapped);
        }

        /// <summary>
        /// Get all enrolled courses for a student by his userid
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseStatusDto[]> GetAllEnrolledCoursesByStudentId([FromQuery] int studentId)
        {
            var courseStatusList = CourseStatusService.GetAllEnrolledCoursesById(studentId);
            if (courseStatusList == null) return StatusCode(StatusCodes.Status404NotFound);

            var mapped = Mapper.Map<CourseStatusDto[]>(courseStatusList);
            return Ok(mapped);
        }
    }
}