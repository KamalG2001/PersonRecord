using PersonRecord.Models;

namespace PersonRecord.Repos
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}