using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.UserDomain
{
    public abstract class User
    {
        [Key]
        public int UserId { get; set; }
        
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}