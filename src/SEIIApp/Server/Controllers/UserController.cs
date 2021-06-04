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

        /// <summary>
        ///  Returns Student by given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> GetUser([FromRoute] int id)
        {
            var user = UserService.GetUserById(id);
            if (user == null) return StatusCode(StatusCodes.Status404NotFound);
            if (user.GetType() == typeof(Student))
            {
                var mappedStudent = Mapper.Map<StudentDto>(user);
                return Ok(mappedStudent);

            }
            else
            {
                var mappedStudent = Mapper.Map<InstructorDto>(user);
                return Ok(mappedStudent);

            }

            
        }
        
        /// <summary>
        /// Return Student by given Name and Password
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> GetUserByNameAndPw([FromQuery] string name, [FromQuery] string password)
        {
            var user = UserService.GetUserByNameAndPw(name, password);
            if (user == null) return StatusCode(StatusCodes.Status404NotFound);


            if (user.GetType() == typeof(Student))
            {
                var mappedStudent = Mapper.Map<StudentDto>(user);
                return Ok(mappedStudent);

            }
            else
            {
                var mappedStudent = Mapper.Map<InstructorDto>(user);
                return Ok(mappedStudent);

            }
            

            
            
        }

        /// <summary>
        /// Returns all users who are students
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/allstudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StudentDto[]> GetAllStudents()
        {
            var students = UserService.GetAllStudents();
            var mapped = Mapper.Map<StudentDto[]>(students);
            return Ok(mapped);
        }
    }
}