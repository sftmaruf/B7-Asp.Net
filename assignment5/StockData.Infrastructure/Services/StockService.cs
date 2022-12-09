using StockData.Infrastructure.Entities;
using StockData.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public StockService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void SaveTradingData(List<List<object>> rows)
        {
            foreach (var row in rows)
            {
                int col = 2;
                var stockPrices = new List<StockPrice>();

                while (col < row.Count)
                {
                    var stockPrice = new StockPrice();
                    foreach (var property in stockPrice.GetType().GetProperties())
                    {
                        if (property.Name != "Id" && property.Name != "CompanyId"
                            && property.Name != "Company")
                        {
                            property.SetValue(stockPrice,
                                Convert.ChangeType(row[col], property.PropertyType));

                            col++;
                        }
                    }
                    stockPrices.Add(stockPrice);
                }

                var company = new Company
                {
                    TradeCode = (string)row[1],
                    StockPrices = stockPrices
                };

                _applicationUnitOfWork.Companies.Add(company);
                _applicationUnitOfWork.Save();
            }
        }
    }
}
