namespace StockData.Infrastructure.Services
{
    public interface IStockService
    {
        void SaveTradingData(List<List<object>> rows);
    }
}