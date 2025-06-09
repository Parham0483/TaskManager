using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        bool SaveChanges();
        IEnumerable<Tasks> GetAllTasks();
        Tasks? GetTaskById(int id);
        IEnumerable<Tasks> GetTasksByUserId(int userId);
        IEnumerable<Tasks> GetTasksByStatus(Models.TaskStatus status);
        IEnumerable<Tasks> GetTasksByUserIdAndStatus(int userId, Models.TaskStatus status);
        IEnumerable<Tasks> GetTasksByDateCreated(DateTime date);
        IEnumerable<Tasks> GetTasksByDateHandedIn(DateTime date);
        void CreateTask(Tasks task);
        void UpdateTask(Tasks task);
        void DeleteTask(int id);
    }
}
