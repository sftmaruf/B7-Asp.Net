using FirstDemo.Infrastructure.UnitOfWorks;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork
    {
        public IBookRepository Books { get; private set; }
        public IReaderRepository Readers { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext, IBookRepository booksRepository, IReaderRepository readerRepository) : base((DbContext) dbContext)
        {
            Books = booksRepository;
            Readers = readerRepository;
        }
    }
}
