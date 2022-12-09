using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; set; }
    }
}
