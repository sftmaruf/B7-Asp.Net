using Autofac;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Repositories;
using StockData.Infrastructure.Services;
using StockData.Infrastructure.UnitOfWorks;

namespace StockData.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public InfrastructureModule(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>()
                .As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>()
                .As<ICompanyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>()
                .As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockService>()
                .As<IStockService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
