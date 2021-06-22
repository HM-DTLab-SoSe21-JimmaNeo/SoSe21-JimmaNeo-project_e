using System;

namespace SEIIApp.Shared.DomainDto.StatusDto
{
    public class QuizStatusDto
    {
        public QuizDto Quiz { get; set; }

        public bool Finished { get; set; }

        public DateTime LastAnswered { get; set; }
    }
}