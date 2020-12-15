namespace WebService.ObjectDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Surname { get; set; } 
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
    }
}