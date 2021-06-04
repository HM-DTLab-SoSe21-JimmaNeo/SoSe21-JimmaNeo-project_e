using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class AnswerDto
    {
        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsSelected { get; set; }
    }
}