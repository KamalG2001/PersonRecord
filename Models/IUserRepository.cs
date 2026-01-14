namespace PersonRecord.Models
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}