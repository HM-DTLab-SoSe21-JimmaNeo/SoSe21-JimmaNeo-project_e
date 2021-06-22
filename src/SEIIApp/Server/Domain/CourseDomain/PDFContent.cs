using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.CourseDomain
{
    public class PdfContent 
    {
        [Key] public int ContentId { get; set; }

        public string ContentName { get; set; }

        public string baseString { get; set; }
    }
}