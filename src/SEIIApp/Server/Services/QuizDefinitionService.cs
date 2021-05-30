using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.Services {
    public class QuizDefinitionService {

        private DatabaseContext DatabaseContext { get; set; }
        private IMapper Mapper { get; set; }
        public QuizDefinitionService(DatabaseContext db, IMapper mapper) {
            this.DatabaseContext = db;
            this.Mapper = mapper;
        }

        private IQueryable<QuizDefinition> GetQueryableForQuizDefinitions() {
            return DatabaseContext
                .QuizDefinitions
                .Include(quiz => quiz.Questions)
                    .ThenInclude(question => question.Answers);

            /* Diese Includes sagen der Datenbank, dass wir mit Joins arbeiten.
             * Wir holen daher aus den Datenbanken, in denen auch die Fragen zu einem Quiz und
             * die Antworten zu den Fragen gespeichert werden, die verbundenen Entitäten
             * aus der Datenbank.
             */
        }

        /// <summary>
        /// Returns all quiz definitions. Includes also questions and their answers.
        /// </summary>
        public QuizDefinition[] GetAllQuizzes() {
            return GetQueryableForQuizDefinitions().ToArray();
        }

        /// <summary>
        /// Returns the quiz with the given id. Includes also questions and their answers.
        /// </summary>
        public QuizDefinition GetQuizWithId(int id) {
            return GetQueryableForQuizDefinitions().Where(quiz => quiz.Id == id).FirstOrDefault();
            //FirstOrDefault liefert das erste gefundene Objekt oder null zurück
        }

        /// <summary>
        /// Adds a quiz.
        /// </summary>
        public QuizDefinition AddQuiz(QuizDefinition quiz) {
            DatabaseContext.QuizDefinitions.Add(quiz);
            DatabaseContext.SaveChanges();
            return quiz;
        }

        /// <summary>
        /// Updates a quiz.
        /// </summary>
        public QuizDefinition UpdateQuiz(QuizDefinition quiz) {
            //Wenn wir ein Quiz aktualisieren, dann fragen wir das existierende Quiz ab und 
            //Mappen die Änderung hinein.

            var existingQuiz = GetQuizWithId(quiz.Id);

            Mapper.Map(quiz, existingQuiz); //we can map into the same object type

            DatabaseContext.QuizDefinitions.Update(existingQuiz);
            DatabaseContext.SaveChanges();
            return existingQuiz;
        }

        /// <summary>
        /// Removes a quiz and all dependencies.
        /// </summary>
        public void RemoveQuiz(QuizDefinition quiz) {
            DatabaseContext.QuizDefinitions.Remove(quiz);
            DatabaseContext.SaveChanges();
        }

    }
}
