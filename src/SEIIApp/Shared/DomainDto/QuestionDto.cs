using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class QuestionDto
    {
        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string QuestionText { get; set; }

        [ValidateComplexType] 
        public AnswerDto[] Answers { get; set; }
        
        public int QuestionStatus { get; set; }
        
        public int QuestionId { get; set; }
    }
}