using System;
using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus
{
    public class QuizStatus
    {
        [Key] public int QuizStatusId { get; set; }

        public Quiz Quiz { get; set; }

        public bool Finished { get; set; }

        public DateTime LastAnswered { get; set; }
    }
}