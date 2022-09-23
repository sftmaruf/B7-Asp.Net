using FirstDemo.Infrastructure.Repositories;
using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReaderRepository : Repository<Reader, Guid>, IReaderRepository
    {
        public ReaderRepository(IApplicationDbContext context) : base((DbContext) context)
        {
        }
    }
}
