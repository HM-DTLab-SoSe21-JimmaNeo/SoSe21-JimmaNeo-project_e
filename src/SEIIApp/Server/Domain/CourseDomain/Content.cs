using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class Content
    {
        [Key]
        public int ContentId { get; set; }

        public string Path { get; set; }


    }
}
