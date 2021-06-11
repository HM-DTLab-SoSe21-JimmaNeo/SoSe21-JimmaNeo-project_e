using System.Linq;
using AutoMapper;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Services
{
    public class PdfContentService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public PdfContentService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }

        public IQueryable<PdfContent> GetQueryableForPdfContent()
        {
            return DatabaseContext.PdfContents;
        }

        public PdfContent GetPdfContentById(int id)
        {
            return GetQueryableForPdfContent().FirstOrDefault(x => x.ContentId == id);
        }

        public PdfContent AddPdfContent(PdfContent content)
        {
            DatabaseContext.PdfContents.Add(content);
            DatabaseContext.SaveChanges();
            return content;
        }

        public PdfContent UpdateContent(PdfContent newContent)
        {
            var existingContent = GetPdfContentById(newContent.ContentId);

            Mapper.Map(newContent, existingContent);

            DatabaseContext.PdfContents.Update(existingContent);
            DatabaseContext.SaveChanges();
            return existingContent;
        }

        public PdfContent[] GetAllContent()
        {
            return GetQueryableForPdfContent().ToArray();
        }
    }
}