using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SEIIApp.Server.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace SEIIApp.Server.Test {

    public class TestQuizDefinitionService {

        private Services.QuizDefinitionService QuizService { get; set; }

        public TestQuizDefinitionService() {
            var serviceCollection = ServiceHelper.GetConfiguredServiceCollection();
            var scope = ServiceHelper.CreateServiceScope(serviceCollection);
            QuizService = scope.ServiceProvider.GetRequiredService<Services.QuizDefinitionService>();
        }

        [Fact]
        public void AddQuiz() {
            Domain.QuizDefinition quiz = new Domain.QuizDefinition();
            quiz.QuizName = "My first quiz";
            QuizService.AddQuiz(quiz);
            quiz.Id.Should().NotBe(0); //id should now be set
        }

        [Fact]
        public void AddQuestionToQuiz() {
            //first way
            Domain.QuizDefinition quiz = new Domain.QuizDefinition();
            quiz.QuizName = "My second quiz";
            quiz.Questions = new() {
                new Domain.QuestionDefinition() { QuestionText = "q1" }
            };

            QuizService.AddQuiz(quiz);
            quiz.Id.Should().NotBe(0);
            //question id should also not be 0/zero
            quiz.Questions[0].Id.Should().NotBe(0);
        }

        [Fact]
        public void TestRemoveQuiz() {
            var quiz = DataAccess.TestDataGenerator.CreateQuizDefinition();
            QuizService.AddQuiz(quiz);
            quiz.Id.Should().NotBe(0);

            quiz = QuizService.GetQuizWithId(quiz.Id); //ask database for that quiz

            //remove that question
            QuizService.RemoveQuiz(quiz);

            //query again
            quiz = QuizService.GetQuizWithId(quiz.Id);
            quiz.Should().BeNull();
        }

    }
}
