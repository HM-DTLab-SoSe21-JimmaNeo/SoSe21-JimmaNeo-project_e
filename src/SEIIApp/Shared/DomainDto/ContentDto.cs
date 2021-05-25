using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class ContentDto
    {
        public string Path { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ContentName { get; set; }
    }
}