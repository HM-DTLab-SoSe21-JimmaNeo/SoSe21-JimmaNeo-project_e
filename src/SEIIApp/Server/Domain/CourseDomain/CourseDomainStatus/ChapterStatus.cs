using System;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus
{
    public class ChapterStatus
    {
        [Key] public int ChapterStatusId { get; set; }

        public Chapter Chapter { get; set; }

        public bool Finished { get; set; }
        
        public DateTime LastWorkedOn { get; set; }

        //public QuizStatus QuizStatus { get; set; }
    }
}