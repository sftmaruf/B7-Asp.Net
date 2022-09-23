using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Library.Areas.Admin.Models
{
    public class BookCreateModel
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? AuthorName { get; set; }
        public DateTime ReleaseDate { get; set; }

        private ILifetimeScope _scope;
        private IBookService _bookService;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _bookService = _scope.Resolve<IBookService>();
        }

        public async Task CreateBook()
        {
            Book book = new Book();
            book.Name = Name;
            book.Price = Price;
            book.AuthorName = AuthorName;
            book.ReleaseDate = ReleaseDate;

            _bookService.Create(book);
        }
    }
}
