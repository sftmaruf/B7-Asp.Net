using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Library.Areas.Admin.Models
{
    public class ReaderCreateModel
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        private ILifetimeScope _scope;
        private IReaderService _readerService;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _readerService = _scope.Resolve<IReaderService>();
        }

        public async Task CreateReader()
        {
            Reader reader = new Reader();
            reader.Name = Name;
            reader.Age = Age;
            reader.Address = Address;

            _readerService.Create(reader);
        }
    }
}
