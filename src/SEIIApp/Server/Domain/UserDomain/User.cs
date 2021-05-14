using System.ComponentModel.DataAnnotations;

namespace SEIIApp.Server.Domain.UserDomain
{
    public abstract class User
    {
        [Key]
        public int UserId { get; set; }
    }
}