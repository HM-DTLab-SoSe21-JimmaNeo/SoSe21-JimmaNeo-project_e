using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Services;
using SEIIApp.Server.Services.StatusServices;
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
        /// Adds or updates a course status (= enroll a student in a course or set new finishStatus or LastWorkedOn)
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CourseStatusDto> AddOrUpdateCourseStatus([FromQuery] int courseId,
            [FromQuery] int studentId)
        {
            var course = CourseService.GetCourseById(courseId);
            var student = UserService.GetStudentById(studentId);

            var result = CourseStatusService.AddOrUpdateCourseStatus(course, student);

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

        /// <summary>
        /// Get the CourseStatus for the last course a student worked on 
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/getlast/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseStatus> GetLastCourseStatusWorkedOn([FromRoute] int id)
        {
            var student = UserService.GetStudentById(id);
            var courseStatus = CourseStatusService.GetLastCourseStatusWorkedOn(student);
            return courseStatus;
        }
    }
}