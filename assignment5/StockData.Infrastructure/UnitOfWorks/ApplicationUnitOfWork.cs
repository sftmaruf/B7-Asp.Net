using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext applicationDbContext,
            ICompanyRepository companyRepository) : base((DbContext)applicationDbContext)
        {
            Companies = companyRepository;
        }
    }
}
