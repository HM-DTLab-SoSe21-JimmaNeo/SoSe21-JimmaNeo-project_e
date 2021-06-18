namespace SEIIApp.Shared.DomainDto
{
    public class QuizStatusTransfer
    {
        public int quizId { get; set; }
        public int studentId { get; set; }
        public bool finished { get; set; }
    }
}