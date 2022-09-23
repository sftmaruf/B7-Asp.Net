using Infrastructure.UnitOfWorks;
using BookBO = Infrastructure.BusinessObjects.Book;
using BookEO = Infrastructure.Entities.Book;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private IApplicationUnitOfWork _applicationUnitOfWork;

        public BookService(IApplicationUnitOfWork applicaitonUnitOfWork)
        {
            _applicationUnitOfWork = applicaitonUnitOfWork;
        }

        public void Create(BookBO book)
        {
            BookEO bookEntity = new BookEO();
            bookEntity.Name = book.Name;
            bookEntity.Price = book.Price;
            bookEntity.AuthorName = book.AuthorName;
            bookEntity.ReleaseDate = book.ReleaseDate;

            _applicationUnitOfWork.Books.Add(bookEntity);
            _applicationUnitOfWork.Save();
        }

        public (int total, int totalDisplay, IList<BookBO> records) GetBooks(int pageIndex,
           int pageSize, string searchText, string orderby)
        {
            (IList<BookEO> data, int total, int totalDisplay) results = _applicationUnitOfWork.Books.GetBooks(pageIndex, pageSize, searchText, orderby);

            IList<BookBO> books = new List<BookBO>();
            foreach (BookEO bookEO in results.data)
            {
                books.Add(new BookBO
                {
                    Id = bookEO.Id,
                    Name = bookEO.Name,
                    Price = bookEO.Price,
                    AuthorName = bookEO.AuthorName,
                    ReleaseDate = bookEO.ReleaseDate
                });
            }

            return (results.total, results.totalDisplay, books);
        }
    }
}
