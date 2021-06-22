using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Shared.DomainDto
{
    public class PdfContentDto
    {
     
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ContentName { get; set; }
        
        public int ContentId { get; set; }
        
        public string baseString { get; set; }
    }
}