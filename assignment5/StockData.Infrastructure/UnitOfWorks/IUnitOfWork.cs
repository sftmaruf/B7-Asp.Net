namespace StockData.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose();
        void Save();
    }
}