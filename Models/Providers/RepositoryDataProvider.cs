namespace PersonRecord.Models.Providers
{
   
    public class RepositoryDataProvider : IDataProvider
    {
        private readonly IUserRepository _repository;
        public string ProviderName => "Repository Provider";
        public RepositoryDataProvider(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<User> GetAllUsers() => _repository.GetAllUsers();
        public void AddUser(User user) => _repository.AddUser(user);
        public void UpdateUser(User user) => _repository.UpdateUser(user);
        public void DeleteUser(User user) => _repository.DeleteUser(user);
    }
}