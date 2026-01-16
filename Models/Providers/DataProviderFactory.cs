namespace PersonRecord.Models.Providers
{
    public class DataProviderFactory
    {
        public static IDataProvider CreateProvider(StorageType storageType, IUserRepository? repository = null)
        {
            return storageType switch
            {
                StorageType.Memory => new MemoryDataProvider(),
                
                StorageType.Repository => repository != null 
                    ? new RepositoryDataProvider(repository)
                    : throw new ArgumentNullException(nameof(repository), "Repository is required for Repository storage type"),
                
                _ => throw new ArgumentException($"Unknown storage type: {storageType}")
            };
        }
    }
}