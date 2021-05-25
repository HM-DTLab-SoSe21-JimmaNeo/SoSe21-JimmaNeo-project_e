using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class ChapterDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ChapterName { get; set; }

        public QuizDto ChapterQuiz { get; set; }

        public ContentDto[] ChapterContent { get; set; }
    }
}