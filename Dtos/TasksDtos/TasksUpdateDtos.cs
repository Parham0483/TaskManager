namespace TaskManager.Dtos
{
    public class TasksUpdateDto
    {
        public string? Status { get; set; } 
        public string? Asignee { get; set; } 
        public DateTime? HandedIn { get; set; } 
    }
}