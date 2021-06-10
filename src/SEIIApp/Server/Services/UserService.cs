using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.CourseDomain.CourseDomainStatus;
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
        
        private IQueryable<User> GetQueryableForUsers()
        {
            return DatabaseContext
                .Users;
        }

        private IQueryable<Student> GetQueryableForStudent()
        {
            return DatabaseContext
                .Students
                .Include(x => x.QuestionStatusList)
                .ThenInclude(x => x.Question)
                .ThenInclude(x => x.Answers)
                .Include(x => x.EnrolledCourses)
                .ThenInclude(a => a.Course)
                .Include(x => x.ChapterStatuslist)
                .ThenInclude(x => x.Chapter)
                .Include(x => x.QuizStatusList)
                .ThenInclude(x => x.Quiz);
        }
        
        public User GetUserById(int id)
        {
            return GetQueryableForUsers().FirstOrDefault(x => x.UserId == id);
        }
        
        public User GetUserByNameAndPw(string name, string password)
        {
            return GetQueryableForUsers()
                .FirstOrDefault(x => x.Password.Equals(password) && x.UserName.Equals(name));
        }
        
        public User AddUser(User newUser)
        {
            DatabaseContext.Users.Add(newUser);
            DatabaseContext.SaveChanges();
            return newUser;
        }

        public void RemoveUser(User toRemove)
        {
            DatabaseContext.Users.Remove(toRemove);
            DatabaseContext.SaveChanges();
        }

        public User UpdateUser(User user)
        {
            var existingUser = GetUserById(user.UserId);

            Mapper.Map(user, existingUser);
            DatabaseContext.Users.Update(existingUser);
            DatabaseContext.SaveChanges();
            return existingUser;
        }

        public User[] GetAllUsers()
        {
            return GetQueryableForUsers().ToArray();
        }

        
        public Student[] GetAllStudents()
        {
            return GetQueryableForUsers().OfType<Student>().ToArray();
        }

        public Student GetStudentById(int id)
        {
            return GetQueryableForStudent().FirstOrDefault(x => x.UserId == id);
        }
        
        


    }
}