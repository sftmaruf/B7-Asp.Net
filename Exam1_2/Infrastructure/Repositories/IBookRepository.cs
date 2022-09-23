using FirstDemo.Infrastructure.Repositories;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        (IList<Book> data, int total, int totalDisplay) GetBooks(int pageIndex,
            int pageSize, string searchText, string orderby);
    }
}