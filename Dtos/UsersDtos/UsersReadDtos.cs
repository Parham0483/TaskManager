namespace TaskManager.Dtos
{

    public class UsersReadDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; }
        public int AssignedTasksCount { get; set; }
 
    }
}
