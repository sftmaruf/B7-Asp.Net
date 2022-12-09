using Autofac;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockData.Infrastructure.Entities;
using StockData.Infrastructure.UnitOfWorks;
using StockData.Worker.Models;
using System.Diagnostics;
using System.Text;

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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                _scope.Resolve<StockDataModel>().LoadData();

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}