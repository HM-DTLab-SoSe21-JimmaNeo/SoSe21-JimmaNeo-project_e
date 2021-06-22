using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class Quiz
    {
        [Key] public int QuizId { get; set; }

        public string QuizName { get; set; }

        public List<Question> Questions { get; set; }
    }
}