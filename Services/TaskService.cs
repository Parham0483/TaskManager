using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {

        private readonly AppDbContext _context;
        public TaskService(AppDbContext context)
        {
            _context = context;
        }
        public void CreateTask(Tasks task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.Tasks.Add(task);
            SaveChanges();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateTask(Tasks task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.Tasks.Update(task);
            SaveChanges();
        }

        public void PatchTask(int id, Tasks task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var existingTask = GetTaskById(id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }

            // Update properties as needed
            existingTask.Status = task.Status;
            existingTask.AssigneeId = task.AssigneeId;

            if (!string.IsNullOrWhiteSpace(task.Title))
            {
                existingTask.Title = task.Title;
            }
            if (!string.IsNullOrWhiteSpace(task.Description))
            {
                existingTask.Description = task.Description;
            }
            if (task.Status == Models.TaskStatus.Done && existingTask.HandedIn == default)
            {
                existingTask.HandedIn = DateTime.UtcNow; // Set HandedIn date if task is marked as Done and not already set
            }
            else if (task.Status != Models.TaskStatus.Done)
            {
                existingTask.HandedIn = default; // Reset HandedIn date if task is not Done
            }
            SaveChanges();
        }
        public void DeleteTask(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid task ID", nameof(id));
            }
            _context.Tasks.Remove(new Tasks { Id = id });
            SaveChanges();
        }
        public IEnumerable<Tasks> GetAllTasks()
        {
            return _context.Tasks.Include(t => t.Assignee).ToList();
        }

        public Tasks GetTaskById(int id)
        {
            return _context.Tasks
            .Include(t => t.Assignee)
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Tasks> GetTasksByDateCreated(DateTime date)
        {
            return _context.Tasks
                .Where(t => t.CreatedAt.Date == date.Date)
                .ToList();
        }

        public IEnumerable<Tasks> GetTasksByDateHandedIn(DateTime date)
        {
            return _context.Tasks
                .Where(t => t.HandedIn.Date == date.Date)
                .ToList();
        }

        public IEnumerable<Tasks> GetTasksByStatus(Models.TaskStatus status)
        {
            return _context.Tasks
                .Where(t => t.Status == status)
                .ToList();
        }

        public IEnumerable<Tasks> GetTasksByUserId(int userId)
        {
            return _context.Tasks
            .Include(t => t.Assignee)
            .Where(t => t.Assignee != null && t.Assignee.Id == userId)
            .ToList();    
        }

    }
}
