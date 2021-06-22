using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class VideoContent
    {
        [Key] public int ContentId { get; set; }

        public string ContentName { get; set; }
        public string Path { get; set; }
    }
}