using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class Chapter
    {
        [Key] public int ChapterId { get; set; }

        public string ChapterName { get; set; }

        public Quiz ChapterQuiz { get; set; }

        public List<Content> ChapterContent { get; set; }
    }
}