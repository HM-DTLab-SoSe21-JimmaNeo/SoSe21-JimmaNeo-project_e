using System;

namespace SEIIApp.Shared.DomainDto.StatusDto
{
    public class QuestionStatusDto
    {
        public QuestionDto Question { get; set; }

        public int QuestionLevel { get; set; }

        public DateTime LastAnswered { get; set; }
    }
}