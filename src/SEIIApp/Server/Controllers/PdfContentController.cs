using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/pdfcontent")]
    public class PdfContentController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        
        private PdfContentService PdfContentService { get; set; }

        public PdfContentController(PdfContentService pdfContentService, IMapper mapper)
        {
            this.Mapper = mapper;
            this.PdfContentService = pdfContentService;
        }
        
        /// <summary>
        /// Update or Add pdf content
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PdfContentDto> AddOrUpdateContent([FromBody] PdfContentDto model)
        {
            if (ModelState.IsValid)
            {
                var mappedmodel = Mapper.Map<PdfContent>(model);
                
                if (model.ContentId == 0)
                {
                    mappedmodel = PdfContentService.AddPdfContent(mappedmodel);
                }
                else
                {
                    mappedmodel = PdfContentService.UpdateContent(mappedmodel);
                }

                model = Mapper.Map<PdfContentDto>(mappedmodel);
                return Ok(model);
            }

            return BadRequest(ModelState);
        }
        
        /// <summary>
        /// Get pdf content by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PdfContentDto> GetContent([FromRoute] int id)
        {
            var content = PdfContentService.GetPdfContentById(id);
            if(content == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedCourse = Mapper.Map<PdfContentDto>(content);

            return Ok(mappedCourse);
        }
        
        /// <summary>
        /// Get all pdf content
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PdfContentDto[]> GetAllContent()
        {
            var content = PdfContentService.GetAllContent();
            var mapped = Mapper.Map<PdfContentDto[]>(content);
            return Ok(mapped);
        }
        
    }
}