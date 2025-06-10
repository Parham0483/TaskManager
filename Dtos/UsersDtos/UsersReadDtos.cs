namespace TaskManager.Dtos
{

    public class UsersReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public int AssignedTasksCount { get; set; }
        public string Role { get; set; } = "User";
 
    }
}
