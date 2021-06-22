using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.Controllers
{
    [ApiController]
    [Route("api/chapter")]
    public class ChapterController : ControllerBase
    {
        private ChapterService ChapterService { get; set; }
        private IMapper Mapper { get; set; }

        public ChapterController(ChapterService cs, IMapper im)
        {
            this.Mapper = im;
            this.ChapterService = cs;
        }

        /// <summary>
        /// Get a chapter by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ChapterDto> GetChapter([FromRoute] int id)
        {
            var chapter = ChapterService.GetChapterById(id);
            if (chapter == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedChapter = Mapper.Map<ChapterDto>(chapter);

            return Ok(mappedChapter);
        }
        
        /// <summary>
        /// Get all chapters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ChapterDto[]> GetAllChapters()
        {
            var chapters = ChapterService.GetAllChapters();
            var mapped = Mapper.Map<ChapterDto[]>(chapters);
            return Ok(mapped);
        }
        
        /// <summary>
        /// Get chapter by quiz id
        /// </summary>
        /// <returns></returns>
        [HttpGet("byquiz/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ChapterDto> GetChapterByQuizId([FromRoute]int id)
        {
            var chapter = ChapterService.GetChapterByQuizId(id);
            var mapped = Mapper.Map<ChapterDto>(chapter);
            return Ok(mapped);
        }
    }
}