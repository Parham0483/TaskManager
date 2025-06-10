namespace TaskManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User"; 
        public ICollection<Tasks> AssignedTasks { get; set; } = new List<Tasks>();
    }
}
