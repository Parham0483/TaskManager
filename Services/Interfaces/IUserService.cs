using TaskManager.Models;

namespace TaskManager.Services
{
    public interface IUserService
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        void PatchUser(int id, User user);
        User? Login(string phoneNo, string password);

    }
}