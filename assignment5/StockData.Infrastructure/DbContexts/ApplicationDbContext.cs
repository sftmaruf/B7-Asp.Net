﻿using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public ApplicationDbContext(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, 
                    m => m.MigrationsAssembly(_assemblyName));
            }
        }

        public DbSet<Company> Companies { get; set; }
    }
}
