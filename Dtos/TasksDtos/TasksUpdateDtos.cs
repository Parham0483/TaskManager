using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class TasksUpdateDto
    {
        public string Title { get; set; }       
        public string Description { get; set; } 
        public Models.TaskStatus? Status { get; set; }
        public int? AssigneeId { get; set; }
        public DateTime? HandedIn { get; set; } 
    }
}