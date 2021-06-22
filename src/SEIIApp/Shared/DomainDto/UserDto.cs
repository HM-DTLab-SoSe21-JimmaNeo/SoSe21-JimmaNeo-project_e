namespace SEIIApp.Shared.DomainDto
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        
        public int UserId { get; set; }
        public bool adminRights { get; set; }
    }
}