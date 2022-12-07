using HtmlAgilityPack;
using System.Text;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                var url = "https://www.dse.com.bd/latest_share_price_scroll_l.php";
                var web = new HtmlWeb();
                var doc = web.Load(url);

                string statusClose = "Closed";

                var marketStatus = doc.DocumentNode
                    .SelectSingleNode("//header/div")
                    .ChildNodes[7]
                    .SelectSingleNode("//span/b").InnerHtml;

                if (marketStatus != statusClose)
                {
                    //Console.WriteLine(doc.ParsedText);
                }
                else
                {
                    var nodes = doc.DocumentNode.SelectSingleNode("//div[@id='RightBody']")
                        .ChildNodes[5]
                        .ChildNodes[3]
                        .ChildNodes[1]
                        .ChildNodes[1]
                        .ChildNodes;

                    foreach (var node in nodes)
                    {
                        if(node.Name == "tbody")
                        {
                            StringBuilder builder = new StringBuilder();
                            var tempNodes = node.ChildNodes[1].ChildNodes;
                            foreach(var n in tempNodes)
                            {
                                if(n.Descendants("a").Any())
                                {
                                    builder
                                        .Append(n.Descendants("a").ToList()[0].InnerHtml.Trim())
                                        .Append(" ");
                                }
                                else
                                {
                                    builder.Append(n.InnerHtml.Trim());
                                    builder.Append(" ");
                                }
                            }
                            
                            Console.WriteLine(builder.ToString());
                        }

                        if (node.Name == "tr")
                        {
                            StringBuilder builder = new StringBuilder();
                            var tempNodes = node.ChildNodes;
                            foreach (var n in tempNodes)
                            {
                                if (n.Descendants("a").Any())
                                {
                                    builder.Append(n.Descendants("a").ToList()[0].InnerHtml.Trim());
                                    builder.Append(" ");
                                }
                                else
                                {
                                    builder.Append(n.InnerHtml.Trim());
                                    builder.Append(" ");
                                }
                            }

                            Console.WriteLine(builder.ToString());
                        }
                    }
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}