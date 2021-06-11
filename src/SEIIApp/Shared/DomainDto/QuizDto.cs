using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class QuizDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string QuizName { get; set; }

        [ValidateComplexType] 
        public QuestionDto[] Questions { get; set; }
        
        public int QuizId { get; set; }
    }
}