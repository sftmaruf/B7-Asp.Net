using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockData.Infrastructure.Entities;
using StockData.Infrastructure.Services;
using StockData.Infrastructure.UnitOfWorks;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace StockData.Worker.Models
{
    public class StockDataModel
    {
        private IStockService _stockService;
        private readonly string _url = "https://www.dse.com.bd/latest_share_price_scroll_l.php";

        public StockDataModel(IStockService stockService)
        {
            _stockService = stockService;
        }

        public void LoadData()
        {
            var web = new HtmlWeb();
            var doc = web.Load(_url);

            string statusClose = "Closed";

            var marketStatus = doc.DocumentNode
                .SelectSingleNode("//header/div")
                .ChildNodes[7]
                .SelectSingleNode("//span/b").InnerHtml;

            var tradingDatas = new List<List<object>>();

            if (marketStatus == statusClose)
            {
                var nodes = doc.DocumentNode.SelectSingleNode("//div[@id='RightBody']")
                    .ChildNodes[5]
                    .ChildNodes[3]
                    .ChildNodes[1]
                    .ChildNodes[1]
                    .ChildNodes;


                foreach (var node in nodes)
                {
                    if (node.Name == "tbody")
                    {
                        var childNodes = node.ChildNodes[1].ChildNodes;
                        tradingDatas.Add(RetriveValuesFromInnerHTML(childNodes));
                    }

                    if (node.Name == "tr")
                    {
                        var childNodes = node.ChildNodes;
                        tradingDatas.Add(RetriveValuesFromInnerHTML(childNodes));
                    }
                }
            }

            _stockService.SaveTradingData(tradingDatas);
        }

        public List<object> RetriveValuesFromInnerHTML(HtmlNodeCollection nodes)
        {
            var values = new List<object>();

            foreach (var node in nodes)
            {
                var anchorDescendants = node.Descendants("a");

                if (anchorDescendants.Any())
                {
                    values.Add(anchorDescendants.ToList()[0]
                        .InnerHtml.Trim());
                }
                else
                {
                    var value = node.InnerHtml.Trim();

                    if (value != string.Empty)
                        values.Add(value);
                }
            }

            return values;
        }
    }
}
