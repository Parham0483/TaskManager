namespace TaskManager.Dtos
{ 
    public class TasksCreateDto
    {
        public string Status { get; set; } = "Todo"; // Default status is "Todo"
        public string Asignee { get; set; } = string.Empty; // Asignee's name
        public DateTime HandedIn { get; set; } // Deadline for the task
    }
}