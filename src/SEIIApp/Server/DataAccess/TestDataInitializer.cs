using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.DataAccess {

    public static class TestDataInitializer {

        /// <summary>
        /// Initialze test data (just for in-memory database)
        /// </summary>
        public static void InitializeTestData(Services.QuizDefinitionService quizDefinitionService) {
            for (int i = 0; i < 10; i++) {
                var quiz = TestDataGenerator.CreateQuizDefinition();
                quiz.QuizName = "Quiz " + i;
                quizDefinitionService.AddQuiz(quiz);
            }

        }

    }

}
