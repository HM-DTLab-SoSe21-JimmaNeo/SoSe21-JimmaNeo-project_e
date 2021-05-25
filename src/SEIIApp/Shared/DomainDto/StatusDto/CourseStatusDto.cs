namespace SEIIApp.Shared.DomainDto.StatusDto
{
    public class CourseStatusDto
    {
        public CourseDto Course { get; set; }
        public ChapterStatusDto[] ChapterStatusList { get; set; }

        public int FinishStatus { get; set; }
    }
}