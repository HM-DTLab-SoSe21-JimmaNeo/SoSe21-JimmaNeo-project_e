using System.Linq;
using AutoMapper;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;

namespace SEIIApp.Server.Services
{
    public class VideoContentService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public VideoContentService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }

        public IQueryable<VideoContent> GetQueryableForVideoContent()
        {
            return DatabaseContext.VideoContents;
        }

        public VideoContent GetVideoContentById(int id)
        {
            return GetQueryableForVideoContent().FirstOrDefault(x => x.ContentId == id);
        }

        public VideoContent AddVideoContent(VideoContent content)
        {
            DatabaseContext.VideoContents.Add(content);
            DatabaseContext.SaveChanges();
            return content;
        }

        public VideoContent UpdateContent(VideoContent newContent)
        {
            var existingContent = GetVideoContentById(newContent.ContentId);

            Mapper.Map(newContent, existingContent);

            DatabaseContext.VideoContents.Update(existingContent);
            DatabaseContext.SaveChanges();
            return existingContent;
        }

        public VideoContent[] GetAllContent()
        {
            return GetQueryableForVideoContent().ToArray();
        }
    }
}