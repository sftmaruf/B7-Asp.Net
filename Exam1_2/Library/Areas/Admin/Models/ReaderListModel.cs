using Autofac;
using Infrastructure.Services;
using Library.Models;

namespace Library.Areas.Admin.Models
{
    public class ReaderListModel : BaseModel
    {
        private IReaderService? _readerService;

        public ReaderListModel() { }

        public ReaderListModel(IReaderService readerService)
        {
            _readerService = readerService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _readerService = _scope.Resolve<ReaderService>();
        }

        internal object? GetPagedBooks(DataTablesAjaxRequestModel model)
        {

            var data = _readerService.GetReaders(
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
                                record.Age.ToString(),
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
