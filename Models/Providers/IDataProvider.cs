using System.Collections.Generic;

namespace PersonRecord.Models.Providers
{

    public interface IDataProvider
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        string ProviderName { get; }
    }
}