using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class TasksCreateDto
    {
        public string Title { get; set; }     
        public string Description { get; set; } 
        public Models.TaskStatus Status { get; set; } = Models.TaskStatus.Todo;
        public int AssigneeId { get; set; }
        public DateTime HandedIn { get; set; } 
    }
}