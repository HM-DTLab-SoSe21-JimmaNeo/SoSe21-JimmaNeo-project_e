using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;
using SEIIApp.Server.Services;
using SEIIApp.Server.Services.StatusServices;
using SEIIApp.Shared.DomainDto;

namespace SEIIApp.Server.DataAccess
{
    public static class TestData
    {
        public static void CreateTestData(QuizService qs, UserService us, CourseService cs,
            QuestionService questionService, QuestionStatusService questionStatusService, CourseStatusService courseStatusService, ContentService contentService)
        {
            var question1 = new Question() {QuestionText = "Frage 1"};

            var student1 = new Student()
                {UserName = "Peter", Password = "123", QuestionStatusList = new List<QuestionStatus>(),adminRights = false, EnrolledCourses = new List<CourseStatus>()};

            var student2 = new Student()
                {UserName = "Hannah", Password = "456", QuestionStatusList = new List<QuestionStatus>(), adminRights = false};

            var instructor1 = new Instructor() {UserName = "Hr. Meier", Password = "passwort",adminRights = true};

            var quiz1 = new Quiz() {QuizName = "1. Quiz", Questions = new List<Question>() {question1}};

            var chapter1 = new Chapter() {ChapterName = "Chapter 1", ChapterQuiz = quiz1};

            var course1 = new Course() {CourseName = "1. Course", Chapters = new List<Chapter>() {chapter1}};

            us.AddUser(student1);
            us.AddUser(student2);
            us.AddUser(instructor1);

            cs.AddCourse(course1);

            questionStatusService.AddOrUpdateQuestionStatus(question1, student1, 1);

            var xyz = questionStatusService.GetAllQuestionStatusOfUser(2);

            string stringResult;
            
            var filePath1 =
                @"examplefiles\Musterbogen_EingangstestatKUM-LS-BLS2-KS.pdf";

            Byte[] bytes = File.ReadAllBytes(filePath1);

            String dataurl = $"data:pdf;base64,{Convert.ToBase64String(bytes)}";

            var filePath2 =
                @"examplefiles\Working Backwards PR 1-pager.pdf";
             
            Byte[] bytes2 = File.ReadAllBytes(filePath2);
            
            String dataurl2 = $"data:pdf;base64,{Convert.ToBase64String(bytes2)}";
          

            Content file1 = new Content() {ContentName = "Musterbogen_EingangstestatKUM-LS-BLS2-KS", Path = dataurl};
            
            Content file2 = new Content() {ContentName = "Working Backwards PR 1-pager", Path = dataurl2};

            contentService.AddContent(file1);
            contentService.AddContent(file2);

            courseStatusService.AddOrUpdateCourseStatus(course1, student1);



        }
    }
}