using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class TasksUpdateDto
    {
        public Models.TaskStatus? Status { get; set; }
        public int? AssigneeId { get; set; }
        public DateTime? HandedIn { get; set; } 
    }
}