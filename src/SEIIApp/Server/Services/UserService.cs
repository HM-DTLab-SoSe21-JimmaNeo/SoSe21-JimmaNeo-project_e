using System.Linq;
using AutoMapper;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Services
{
    public class UserService
    {
        private DatabaseContext DatabaseContext { get; set; }

        private IMapper Mapper { get; set; }

        public UserService(DatabaseContext db, IMapper m)
        {
            this.DatabaseContext = db;
            this.Mapper = m;
        }
        
        private IQueryable<Student> GetQueryableForStudent()
        {
            return DatabaseContext
                .Students;
        }
        
        public Student GetStudentById(int id)
        {
            return GetQueryableForStudent().FirstOrDefault(x => x.UserId == id);
        }

        public Student GetStudentByNameAndPw(string name, string password)
        {
            return GetQueryableForStudent()
                .FirstOrDefault(x => x.Password.Equals(password) && x.StudentName.Equals(name));
        }
        
        public Student AddStudent(Student newStudent)
        {
            DatabaseContext.Students.Add(newStudent);
            DatabaseContext.SaveChanges();
            return newStudent;
        }
    }
}