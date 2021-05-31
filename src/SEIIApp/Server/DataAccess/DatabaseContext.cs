using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
using SEIIApp.Server.Domain.UserDomain;


namespace SEIIApp.Server.DataAccess
{
    public class DatabaseContext : DbContext

    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<ChapterStatus> ChapterStatus { get; set; }

        public DbSet<CourseStatus> CourseStatus { get; set; }

        public DbSet<QuestionStatus> QuestionStatus { get; set; }

        public DbSet<QuizStatus> QuizStatus { get; set; }
        
        public DbSet<Student> Students { get; set; }
        
        public DbSet<Instructor> Instructors { get; set; }
        
        
    }
}