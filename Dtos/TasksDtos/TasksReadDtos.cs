using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class TasksReadDto
    {
        public int Id { get; set; }
        public string Status { get; set; } 
        public int AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime HandedIn { get; set; }
    }

}
