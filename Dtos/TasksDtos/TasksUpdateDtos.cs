namespace TaskManager.Dtos
{
    public class UpdateTaskDto
    {
        public string? Status { get; set; } 
        public string? Asignee { get; set; } 
        public DateTime? HandedIn { get; set; } 
    }
}