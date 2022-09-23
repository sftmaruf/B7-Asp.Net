using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IBookService
    {
        void Create(Book book);
        (int total, int totalDisplay, IList<Book> records) GetBooks(int pageIndex,
           int pageSize, string searchText, string orderby);
    }
}