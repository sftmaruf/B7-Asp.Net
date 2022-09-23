using FirstDemo.Infrastructure.Repositories;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
    }
}