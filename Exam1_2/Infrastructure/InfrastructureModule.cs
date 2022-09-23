using Autofac;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWorks;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        private string _connectionString;
        private string _assemblyName;

        public InfrastructureModule(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;   
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReaderRepository>().As<IReaderRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<BookRepository>().As<BookRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
