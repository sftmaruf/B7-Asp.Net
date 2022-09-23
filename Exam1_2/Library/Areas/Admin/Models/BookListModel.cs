using Autofac;
using Infrastructure.Services;
using Library.Models;

namespace Library.Areas.Admin.Models
{
    public class BookListModel : BaseModel
    {
        private IBookService? _bookService;

        public BookListModel() : base() { }

        public BookListModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _bookService = _scope.Resolve<IBookService>();
        }

        internal object? GetPagedBooks(DataTablesAjaxRequestModel model)
        {

            var data = _bookService.GetBooks(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Name", "Price", "AuthorName", "ReleaseDate" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Price.ToString(),
                                record.AuthorName,
                                record.ReleaseDate.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
