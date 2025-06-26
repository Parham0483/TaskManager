using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class TasksCreateDto
    {
        public string Title { get; set; }     
        public string Description { get; set; } 
        public string Status { get; set; } = "Todo";
        public int AssigneeId { get; set; }
        public DateTime HandedIn { get; set; } 
    }
}