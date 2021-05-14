using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }
    }
}