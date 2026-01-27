using Autofac;
using PersonRecord.Models;
using PersonRecord.Repos;

namespace PersonRecord.Services
{
    public static class ContainerSetup
    {
        private const bool UseInMemoryRepository = false;  // <-- Change to "true" for in-memory
                                                           // <-- Change to "false" for json file repository
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            if (UseInMemoryRepository)
            {
                builder.RegisterType<InMemoryUserRepository>()
                    .As<IUserRepository>()
                    .SingleInstance();
            }
            else
            {
                builder.RegisterType<JsonUserRepository>()
                    .As<IUserRepository>()
                    .SingleInstance();
            }

            builder.RegisterType<UserManager>()
                .As<IUserService>()
                .SingleInstance();

            builder.RegisterType<FileDialogService>()
                .As<IFileDialogService>()
                .SingleInstance();

            return builder.Build();
        }
    }
}