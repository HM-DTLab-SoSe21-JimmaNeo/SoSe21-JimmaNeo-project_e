using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseDto> GetCourseById([FromRoute] int id)
        {
            var course = CourseService.GetCourseById(id);
            if (course == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedCourse = Mapper.Map<CourseDto>(course);
            return Ok(mappedCourse);
        }
    }
}