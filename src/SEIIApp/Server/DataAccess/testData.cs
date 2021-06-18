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
            QuestionService questionService, QuestionStatusService questionStatusService,
            CourseStatusService courseStatusService, PdfContentService pdfContentService,
            ChapterStatusService chapterStatusService, QuizStatusService quizStatusService,
            VideoContentService videoContentService)
        {
            var filePath1 =
                @"examplefiles\Musterbogen_EingangstestatKUM-LS-BLS2-KS.pdf";

            Byte[] bytes = File.ReadAllBytes(filePath1);

            String dataurl = $"data:pdf;base64,{Convert.ToBase64String(bytes)}";

            var filePath2 =
                @"examplefiles\Working Backwards PR 1-pager.pdf";

            Byte[] bytes2 = File.ReadAllBytes(filePath2);

            String dataurl2 = $"data:pdf;base64,{Convert.ToBase64String(bytes2)}";


            var file1 = new PdfContent()
                {ContentName = "Musterbogen_EingangstestatKUM-LS-BLS2-KS", baseString = dataurl};

            var file2 = new PdfContent() {ContentName = "Working Backwards PR 1-pager", baseString = dataurl2};

            var video1 = new VideoContent()
                {ContentName = "Cool YouTube Video", Path = "https://www.youtube.com/embed/dQw4w9WgXcQ"};

            //contentService.AddContent(file1);
            // contentService.AddContent(file2);


            var answer1 = new Answer() {AnswerText = "Dies ist die richtige Antwort.", IsCorrect = false};
            var answer2 = new Answer() {AnswerText = "Oder ist es diese?", IsCorrect = true};

            var question1 = new Question()
                {QuestionText = "Was stimmt?", Answers = new List<Answer>() {answer1, answer2}};

            var answer3 = new Answer() {AnswerText = "Das ist die richtige Antwort.", IsCorrect = false};
            var answer4 = new Answer() {AnswerText = "Nein, diese ist richtig!", IsCorrect = true};

            var question2 = new Question()
                {QuestionText = "Kreuze das richtige an!", Answers = new List<Answer>() {answer3, answer4}};

            var answer5 = new Answer() {AnswerText = "Rot ist am besten.", IsCorrect = false};
            var answer6 = new Answer() {AnswerText = "Nein, blau ist die beste Farbe!", IsCorrect = true};

            var question3 = new Question()
                {QuestionText = "Was ist die beste Farbe?", Answers = new List<Answer>() {answer5, answer6}};


            var student1 = new Student()
            {
                UserName = "Peter", Password = "123", QuestionStatusList = new List<QuestionStatus>(),
                adminRights = false,
                EnrolledCourses = new List<CourseStatus>(), ChapterStatuslist = new List<ChapterStatus>(),
                QuizStatusList = new List<QuizStatus>()
            };

            var student2 = new Student()
            {
                UserName = "Hannah", Password = "456", QuestionStatusList = new List<QuestionStatus>(),
                adminRights = false
            };

            var instructor1 = new Instructor() {UserName = "Meier", Password = "passwort", adminRights = true};

            var quiz1 = new Quiz() {QuizName = "1. Quiz", Questions = new List<Question>() {question1, question2}};
            var quiz2 = new Quiz() {QuizName = "2. Quiz: Farben", Questions = new List<Question>() {question3}};

            var chapter1 = new Chapter()
            {
                ChapterName = "Chapter 1", ChapterQuiz = quiz1, ChapterContentPdf = new List<PdfContent>() {file1},
                ChapterContentVideo = new List<VideoContent>() {video1}
            };
            var chapter2 = new Chapter()
            {
                ChapterName = "Chapter 2: Gut und schlecht", ChapterQuiz = quiz2,
                ChapterContentPdf = new List<PdfContent>() {file2}
            };

            var course1 = new Course() {CourseName = "Testkurs", Chapters = new List<Chapter>() {chapter1, chapter2}};

            us.AddUser(student1);
            us.AddUser(student2);
            us.AddUser(instructor1);

            cs.AddCourse(course1);

            questionStatusService.AddOrUpdateQuestionStatus(question1, student1, 1,
                DateTime.Now - TimeSpan.FromHours(30));

            //var xyz = questionStatusService.GetAllQuestionStatusOfUser(2);

            //string stringResult;


            courseStatusService.AddOrUpdateCourseStatus(course1, student1);

            chapterStatusService.AddOrUpdateChapterStatus(chapter1, student1);
           // chapterStatusService.AddOrUpdateChapterStatus(chapter2, student1);


            quizStatusService.AddOrUpdateQuizStatus(quiz1, student1, true);
            quizStatusService.AddOrUpdateQuizStatus(quiz2, student1, true);


            courseStatusService.AddOrUpdateCourseStatus(course1, student1);
        }
    }
}