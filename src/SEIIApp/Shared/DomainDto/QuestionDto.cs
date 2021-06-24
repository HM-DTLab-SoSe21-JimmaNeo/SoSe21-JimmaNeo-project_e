using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class QuestionDto
    {
        
        public string QuestionText { get; set; }

      
        public AnswerDto[] Answers { get; set; }


        public int QuestionId { get; set; }
    }
}