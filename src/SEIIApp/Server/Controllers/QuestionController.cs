using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;
using SEIIApp.Shared.DomainDto.StatusDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/question")]
    public class QuestionController : ControllerBase
    {
        private QuestionService QuestionService { get; set; }
        
        private UserService UserService { get; set; }
        private IMapper Mapper { get; set; }

        public QuestionController(QuestionService questionService, IMapper mapper, UserService userService)
        {
            this.QuestionService = questionService;
            this.Mapper = mapper;
            this.UserService = userService;
        }
        

    }
    
    
}