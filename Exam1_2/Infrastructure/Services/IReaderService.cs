using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IReaderService
    {
        void Create(Reader reader);
        (int total, int totalDisplay, IList<Reader> records) GetReaders(int pageIndex,
         int pageSize, string searchText, string orderby);
    }
}