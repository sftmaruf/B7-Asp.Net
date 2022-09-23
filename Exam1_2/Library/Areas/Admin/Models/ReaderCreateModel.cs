using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Library.Areas.Admin.Models
{
    public class ReaderCreateModel : BaseModel
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        private IReaderService _readerService;

        public ReaderCreateModel() : base() { }

        public ReaderCreateModel(IReaderService readerService) 
        {
            _readerService = readerService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
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
