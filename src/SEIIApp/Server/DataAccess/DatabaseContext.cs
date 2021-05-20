using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.Domain.CourseDomain;
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
            // nothing here yet
        }
        
        public DbSet<Quiz> Quiz { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<Student> Students { get; set; }
        




    }
}