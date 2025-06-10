using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void CreateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            // No implementation needed as EF Core tracks changes automatically
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void PatchUser(int id, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUser = GetUserById(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            // Update properties as needed
            existingUser.Name = user.Name;
            existingUser.PhoneNo = user.PhoneNo;
            existingUser.Role = user.Role;

            SaveChanges();
        }
    }
}