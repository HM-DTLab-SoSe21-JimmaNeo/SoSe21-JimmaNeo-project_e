using System.Collections.Generic;
using AutoMapper;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;

namespace SEIIApp.Server.DataAccess
{
    public static class TestData
    {
        public static void CreateTestData(QuizService qs, UserService us, CourseService cs, QuestionService questionService)
        {
            var testQuiz = new Quiz {QuizName = "Test Quiz", Questions = new List<Question>()};

            var q1 = new Question {QuestionText = "Wie geht's?", Answers = new List<Answer>()};

            var a1 = new Answer {AnswerText = "Gut", IsCorrect = true};

            q1.Answers.Add(a1);
            testQuiz.Questions.Add(q1);

            qs.AddQuiz(testQuiz);

            var content1 = new Content {Path = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"};

            cs.AddContent(content1);

            var contentList = new List<Content> {content1};

            var chapter1 = new Chapter
                {ChapterName = "TestChapter", ChapterQuiz = testQuiz, ChapterContent = contentList};

            cs.AddChapter(chapter1);

            var chapterList = new List<Chapter> {chapter1};

            var course1 = new Course {Chapters = chapterList, CourseName = "TestKurs"};

            cs.AddCourse(course1);

            var courseList = new List<Course> {course1};


            var u1 = new Student() {StudentName = "xy", Password = "123",EnrolledCourses = courseList,WorkingQuestions = new List<Question>()};

            us.AddStudent(u1);

            questionService.AddQuestionToUser(q1, u1, 1);
        }
    }
}