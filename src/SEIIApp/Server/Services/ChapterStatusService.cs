using System.Linq;
using AutoMapper;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services
{
    public class ChapterStatusService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        private UserService UserService { get; set; }

        public ChapterStatusService(DatabaseContext db, IMapper m, UserService userService)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
            this.UserService = userService;
        }

        private IQueryable<ChapterStatus> GetQueryableForChapterStatus()
        {
            return DatabaseContext.ChapterStatus;
        }

        private ChapterStatus GetChapterStatusById(int id)
        {
            return GetQueryableForChapterStatus().FirstOrDefault(x => x.ChapterStatusId == id);
        }


    }
}