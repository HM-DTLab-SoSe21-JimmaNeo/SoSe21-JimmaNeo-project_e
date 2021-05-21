using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class ContentDto
    {
        [Required]
        public string Path { get; set; }
    }
}