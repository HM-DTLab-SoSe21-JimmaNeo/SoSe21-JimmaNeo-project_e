using System.Linq;
using AutoMapper;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Services
{
    public class ContentService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public ContentService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }

        public IQueryable<Content> GetQueryableForContent()
        {
            return DatabaseContext.Contents;
        }

        public Content GetContentById(int id)
        {
            return GetQueryableForContent().FirstOrDefault(x => x.ContentId == id);
        }

        public Content AddContent(Content content)
        {
            DatabaseContext.Contents.Add(content);
            DatabaseContext.SaveChanges();
            return content;
        }

        public Content UpdateContent(Content newContent)
        {
            var existingContent = GetContentById(newContent.ContentId);

            Mapper.Map(newContent, existingContent);
            
            DatabaseContext.Contents.Update(existingContent);
            DatabaseContext.SaveChanges();
            return existingContent;
        }

        public Content[] GetAllContent()
        {
            return GetQueryableForContent().ToArray();
        }
    }
}