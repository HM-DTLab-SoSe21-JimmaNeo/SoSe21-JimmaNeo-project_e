using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class QuizDto
    {
        
        public string QuizName { get; set; }

        
        public QuestionDto[] Questions { get; set; }
        
        public int QuizId { get; set; }
    }
}