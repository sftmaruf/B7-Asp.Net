using FirstDemo.Infrastructure.Repositories;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IReaderRepository : IRepository<Reader, Guid>
    {
    }
}