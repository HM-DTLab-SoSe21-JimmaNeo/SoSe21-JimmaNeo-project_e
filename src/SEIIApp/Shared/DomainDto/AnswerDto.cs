using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class AnswerDto
    {
        
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsSelected { get; set; }
    }
}