using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class ChapterDto
    {
        
        public string ChapterName { get; set; }

        public QuizDto ChapterQuiz { get; set; }

        public PdfContentDto[] ChapterContentPdf { get; set; }
        
        public VideoContentDto[] ChapterContentVideo { get; set; }
        
        public int ChapterId { get; set; } 

    }
}