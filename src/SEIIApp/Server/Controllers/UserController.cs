using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private UserService UserService { get; set; }
        private IMapper Mapper { get; set; }

        public UserController(UserService us, IMapper im)
        {
            this.Mapper = im;
            this.UserService = us;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> GetStudent([FromRoute] int id)
        {
            var student = UserService.GetStudentById(id);
            if(student == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedStudent = Mapper.Map<StudentDto>(student);
            return Ok(mappedStudent);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> GetStudentByNameAndPw([FromQuery] string name, [FromQuery]string password)
        {
            var student = UserService.GetStudentByNameAndPw(name, password);
            if(student == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedStudent = Mapper.Map<StudentDto>(student);
            return Ok(mappedStudent);
        }
        
    }
}