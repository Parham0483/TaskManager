namespace TaskManager.Models
{
    public enum TaskStatus
    {
        Todo, Doing, Done
    }

    public class Tasks
    {
        public int Id { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public string Asignee { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime HandedIn { get; set; } = DateTime.UtcNow;
    }
}
