using System;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus
{
    public class QuestionStatus
    {
        [Key]
        public int QuestionStatusId { get; set; }
        
        public Question Question { get; set; }

        public int QuestionLevel { get; set; }

        public DateTime LastAnswered { get; set; }
    }
}