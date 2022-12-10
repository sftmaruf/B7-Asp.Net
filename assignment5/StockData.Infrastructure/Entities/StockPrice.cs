using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Entities
{
    public class StockPrice : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public double LastTradingPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double ClosePrice { get; set; }
        public double YesterdayClosePrice { get; set; }
        public string Change { get; set; }
        public string Trade { get; set; }
        public string Value { get; set; }
        public string Volume { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
