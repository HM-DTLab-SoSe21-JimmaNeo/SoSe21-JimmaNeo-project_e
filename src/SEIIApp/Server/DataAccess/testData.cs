using System.Collections.Generic;
using AutoMapper;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;

namespace SEIIApp.Server.DataAccess
{
    public static class TestData
    {
        public static void CreateTestData(QuizService qs, UserService us)
        {
            var testQuiz = new Quiz {QuizName = "Test Quiz", Questions = new List<Question>()};

            var q1 = new Question {QuestionText = "Wie geht's?", Answers = new List<Answer>()};

            var a1 = new Answer {AnswerText = "Gut", IsCorrect = true};

            q1.Answers.Add(a1);
            testQuiz.Questions.Add(q1);

            qs.AddQuiz(testQuiz);

            var u1 = new Student() {StudentName = "Hans Meier"};

            us.AddStudent(u1);



        }
    }
}