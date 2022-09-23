using FirstDemo.Infrastructure.Repositories;
using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class ReaderRepository : Repository<Reader, Guid>, IReaderRepository
    {
        public ReaderRepository(IApplicationDbContext context) : base((DbContext) context)
        {
        }

        public (IList<Reader> data, int total, int totalDisplay) GetReaders(int pageIndex,
           int pageSize, string searchText, string orderby)
        {
            (IList<Reader> data, int total, int totalDisplay) results =
                GetDynamic(x => x.Name.Contains(searchText), orderby,
                "", pageIndex, pageSize, true);

            return results;
        }
    }
}
