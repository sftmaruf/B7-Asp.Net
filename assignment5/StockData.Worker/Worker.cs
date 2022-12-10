using Autofac;
using StockData.Worker.Models;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILifetimeScope _scope;

        public Worker(ILogger<Worker> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _scope.Resolve<StockDataModel>().LoadData();

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}