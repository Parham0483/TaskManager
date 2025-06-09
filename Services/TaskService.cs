using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        public void CreateTask(Tasks task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public Tasks? GetTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tasks> GetTasksByDateCreated(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tasks> GetTasksByDateHandedIn(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tasks> GetTasksByStatus(Models.TaskStatus status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tasks> GetTasksByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tasks> GetTasksByUserIdAndStatus(int userId, Models.TaskStatus status)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(Tasks task)
        {
            throw new NotImplementedException();
        }
    }
}
