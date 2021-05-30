using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEIIApp.Server.Domain;
using SEIIApp.Server.Services;
using SEIIApp.Shared.DomainTdo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.Controllers {

    [ApiController]
    [Route("api/quizdefinitions")]
    public class QuizDefinitionController : ControllerBase {
       
        private QuizDefinitionService QuizDefinitionService { get; set; }
        private IMapper Mapper { get; set; }

        public QuizDefinitionController(QuizDefinitionService quizDefinitionService, IMapper mapper) {
            this.QuizDefinitionService = quizDefinitionService;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Return the quiz with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Shared.DomainTdo.QuizDefinitionDto> GetQuiz([FromRoute] int id) {
            var quiz = QuizDefinitionService.GetQuizWithId(id);
            if (quiz == null) return StatusCode(StatusCodes.Status404NotFound);

            var mappedQuiz = Mapper.Map<QuizDefinitionDto>(quiz);
            return Ok(mappedQuiz);
        }

        /// <summary>
        /// Returns all quizzes names and ids.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<QuizDefinitionBaseDto[]> GetAllQuizes() {
            var quizzes = QuizDefinitionService.GetAllQuizzes();
            var mappedQuizzes = Mapper.Map<QuizDefinitionBaseDto[]>(quizzes);
            return Ok(mappedQuizzes);
        }

        /// <summary>
        /// Adds or updates a quiz definition.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<QuizDefinitionDto> AddOrUpdateQuiz([FromBody] QuizDefinitionDto model) {
            if (ModelState.IsValid) {
                //Das Modell ist dann valide, wenn es die per Annotation definierten
                //Eigenschaften erfüllt, ansonsten werden wir einen Fehler zurückliefern.

                //Wir "mappen" das gelieferte Modell zu unserer lokalen Domänen-Repräsentation
                var mappedModel = Mapper.Map<QuizDefinition>(model);

                if(model.Id == 0) { //add
                    mappedModel = QuizDefinitionService.AddQuiz(mappedModel);
                }
                else { //update
                    mappedModel = QuizDefinitionService.UpdateQuiz(mappedModel);
                }

                //Wir liefern das geänderte Objekt auch wieder zurück
                model = Mapper.Map<QuizDefinitionDto>(mappedModel);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Removes a quiz definition.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteQuiz([FromRoute] int id) {
            var quiz = QuizDefinitionService.GetQuizWithId(id);
            if (quiz == null) return StatusCode(StatusCodes.Status404NotFound);

            QuizDefinitionService.RemoveQuiz(quiz);
            return Ok();
        }





    }
}
