using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/videocontent")]
    public class VideoContentController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        
        private VideoContentService VideoContentService { get; set; }

        public VideoContentController(VideoContentService videoContentService, IMapper mapper)
        {
            this.Mapper = mapper;
            this.VideoContentService = videoContentService;
        }
        
        /// <summary>
        /// Update or Add video content
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VideoContentDto> AddOrUpdateContent([FromBody] VideoContentDto model)
        {
            if (ModelState.IsValid)
            {
                var mappedmodel = Mapper.Map<VideoContent>(model);
                
                if (model.ContentId == 0)
                {
                    mappedmodel = VideoContentService.AddVideoContent(mappedmodel);
                }
                else
                {
                    mappedmodel = VideoContentService.UpdateContent(mappedmodel);
                }

                model = Mapper.Map<VideoContentDto>(mappedmodel);
                return Ok(model);
            }

            return BadRequest(ModelState);
        }
        
        /// <summary>
        /// Get video content by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VideoContentDto> GetContent([FromRoute] int id)
        {
            var content = VideoContentService.GetVideoContentById(id);
            if(content == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedCourse = Mapper.Map<VideoContentDto>(content);

            return Ok(mappedCourse);
        }
        
        /// <summary>
        /// Get all video content
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VideoContentDto[]> GetAllContent()
        {
            var content = VideoContentService.GetAllContent();
            var mapped = Mapper.Map<VideoContentDto[]>(content);
            return Ok(mapped);
        }
        
    }
}