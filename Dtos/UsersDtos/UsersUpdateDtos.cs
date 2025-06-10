namespace TaskManager.Dtos
{
    public class UsersUpdateDto
    {
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "User"; 
    }
}