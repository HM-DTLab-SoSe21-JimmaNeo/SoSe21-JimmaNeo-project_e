using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class VideoContentDto 
    {
        
        public string ContentName { get; set; }

        public int ContentId { get; set; }
        public string Path { get; set; }
    }
}