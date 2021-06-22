using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private CourseService CourseService { get; set; }
        private IMapper Mapper { get; set; }

        public CourseController(CourseService cs, IMapper im)
        {
            this.Mapper = im;
            this.CourseService = cs;
        }

        /// <summary>
        /// Get a course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseDto> GetCourse([FromRoute] int id)
        {
            var course = CourseService.GetCourseById(id);
            if (course == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedCourse = Mapper.Map<CourseDto>(course);

            return Ok(mappedCourse);
        }

        /// <summary>
        /// Update or Add a course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CourseDto> AddOrUpdateCourse([FromBody] CourseDto model)
        {
            if (ModelState.IsValid)
            {
                var mappedmodel = Mapper.Map<Course>(model);
                if (model.CourseId == 0)
                {
                    mappedmodel = CourseService.AddCourse(mappedmodel);
                }
                else
                {
                    mappedmodel = CourseService.UpdateCourse(mappedmodel);
                }

                model = Mapper.Map<CourseDto>(mappedmodel);
                return Ok(model);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CourseDto[]> GetAllCourses()
        {
            var courses = CourseService.GetAllCourses();
            var mapped = Mapper.Map<CourseDto[]>(courses);
            return Ok(mapped);
        }


        /// <summary>
        /// Return a course by given course name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet ("byname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CourseDto> GetCourseByName([FromQuery] string name)
        {
            var result = CourseService.GetCourseByName(name);
            var mapped = Mapper.Map<CourseDto>(result);
            return Ok(mapped);
        }

        /// <summary>
        /// Return a course by given course name
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        [HttpGet ("bychapterid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CourseDto> GetCourseByName([FromQuery] int chapterId)
        {
            var result = CourseService.GetCourseByChapterId(chapterId);
            var mapped = Mapper.Map<CourseDto>(result);
            return Ok(mapped);
        }
    }
}