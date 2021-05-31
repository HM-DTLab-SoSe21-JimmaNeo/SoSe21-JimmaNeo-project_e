using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class ContentController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        
        private ContentService ContentService { get; set; }

        public ContentController(ContentService contentService, IMapper mapper)
        {
            this.Mapper = mapper;
            this.ContentService = contentService;
        }
        
        /// <summary>
        /// Update or Add a course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ContentDto> AddOrUpdateCourse([FromBody] ContentDto model)
        {
            if (ModelState.IsValid)
            {
                var mappedmodel = Mapper.Map<Content>(model);
                
                if (model.ContentId == 0)
                {
                    mappedmodel = ContentService.AddContent(mappedmodel);
                }
                else
                {
                    mappedmodel = ContentService.UpdateContent(mappedmodel);
                }

                model = Mapper.Map<ContentDto>(mappedmodel);
                return Ok(model);
            }

            return BadRequest(ModelState);
        }
        
        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContentDto> GetContent([FromRoute] int id)
        {
            var content = ContentService.GetContentById(id);
            if(content == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedCourse = Mapper.Map<ContentDto>(content);

            return Ok(mappedCourse);
        }
        
    }
}