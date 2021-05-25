using System;
using System.Collections.Generic;
using AutoMapper;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;

namespace SEIIApp.Server.DataAccess
{
    public static class TestData
    {
        public static void CreateTestData(QuizService qs, UserService us, CourseService cs,
            QuestionService questionService, QuestionStatusService questionStatusService, CourseStatusService courseStatusService)
        {
            var question1 = new Question() {QuestionText = "Frage 1"};

            var student1 = new Student()
                {UserName = "Peter", Password = "123", QuestionStatusList = new List<QuestionStatus>()};

            var student2 = new Student()
                {UserName = "Hannah", Password = "456", QuestionStatusList = new List<QuestionStatus>()};

            var instructor1 = new Instructor() {UserName = "Hr. Meier", Password = "passwort"};

            var quiz1 = new Quiz() {QuizName = "1. Quiz", Questions = new List<Question>() {question1}};

            var chapter1 = new Chapter() {ChapterName = "Chapter 1", ChapterQuiz = quiz1};

            var course1 = new Course() {CourseName = "1. Course", Chapters = new List<Chapter>() {chapter1}};

            us.AddUser(student1);
            us.AddUser(student2);
            us.AddUser(instructor1);

            cs.AddCourse(course1);

            questionStatusService.AddOrUpdateQuestionStatus(question1, student1, 1);

            var xyz = questionStatusService.GetAllQuestionStatusOfUser(2);
        }
    }
}