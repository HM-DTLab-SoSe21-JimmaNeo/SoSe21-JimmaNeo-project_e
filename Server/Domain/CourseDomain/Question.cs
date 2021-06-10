using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class Question
    {
        [Key] public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public List<Answer> Answers { get; set; }
    }
}