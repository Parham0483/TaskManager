namespace TaskManager.Dtos
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;  
        public string Asignee { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime HandedIn { get; set; }
    }

}
