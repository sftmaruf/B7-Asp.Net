using Infrastructure.UnitOfWorks;

using ReaderBO = Infrastructure.BusinessObjects.Reader;
using ReaderEO = Infrastructure.Entities.Reader;

namespace Infrastructure.Services
{
    public class ReaderService : IReaderService
    {
        private IApplicationUnitOfWork _applicationUnitOfWork;

        public ReaderService(IApplicationUnitOfWork applicaitonUnitOfWork)
        {
            _applicationUnitOfWork = applicaitonUnitOfWork;
        }

        public void Create(ReaderBO reader)
        {
            ReaderEO readerEntity = new ReaderEO();
            readerEntity.Name = reader.Name;
            readerEntity.Age = reader.Age;
            readerEntity.Address = reader.Address;

            _applicationUnitOfWork.Readers.Add(readerEntity);
            _applicationUnitOfWork.Save();
        }

        public (int total, int totalDisplay, IList<ReaderBO> records) GetReaders(int pageIndex,
          int pageSize, string searchText, string orderby)
        {
            (IList<ReaderEO> data, int total, int totalDisplay) results = _applicationUnitOfWork.Readers.GetReaders(pageIndex, pageSize, searchText, orderby);

            IList<ReaderBO> readers = new List<ReaderBO>();
            foreach (ReaderEO readerEO in results.data)
            {
                readers.Add(new ReaderBO
                {
                    Id = readerEO.Id,
                    Name = readerEO.Name,
                    Age = readerEO.Age,
                    Address = readerEO.Address,
                });
            }

            return (results.total, results.totalDisplay, readers);
        }
    }
}
