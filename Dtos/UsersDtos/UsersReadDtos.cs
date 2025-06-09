namespace TaskManager.Dtos
{

    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public int AssignedTasksCount { get; set; }
 
    }
}
