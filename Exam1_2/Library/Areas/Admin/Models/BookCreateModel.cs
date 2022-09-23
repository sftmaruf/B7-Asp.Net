using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Library.Areas.Admin.Models
{
    public class BookCreateModel : BaseModel
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? AuthorName { get; set; }
        public DateTime ReleaseDate { get; set; }

        private IBookService _bookService;

        public BookCreateModel() : base() { }

        public BookCreateModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
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
