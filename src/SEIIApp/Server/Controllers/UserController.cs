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
        public ActionResult<UserDto> GetUserByNameAndPw([FromQuery] string name, [FromQuery] string password)
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
        [HttpGet("allstudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StudentDto[]> GetAllStudents()
        {
            var students = UserService.GetAllStudents();
            var mapped = Mapper.Map<StudentDto[]>(students);
            return Ok(mapped);
        }

        /// <summary>
        /// Add a student 
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPut("student")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDto> AddStudent([FromBody] StudentDto studentDto)
        {
            var result = UserService.AddUser(Mapper.Map<Student>(studentDto));
            return Ok(Mapper.Map<StudentDto>(result));
        }
        
        /// <summary>
        /// Add an instructor
        /// </summary>
        /// <param name="instructorDto"></param>
        /// <returns></returns>
        [HttpPut("instructor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDto> AddInstructor([FromBody] InstructorDto instructorDto)
        {
            var result = UserService.AddUser(Mapper.Map<Instructor>(instructorDto));
            return Ok(Mapper.Map<InstructorDto>(result));
        }
        
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("allusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserDto[]> GetAllUsers()
        {
            var users = UserService.GetAllUsers();
            var mapped = Mapper.Map<UserDto[]>(users);
            return Ok(mapped);
        }
        
    }
}