using ChatroomWithRabbitMq.Core.StockBot;
using ChatroomWithRabbitMq.Models;
using ChatroomWithRabbitMq.Models.Dto;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Formats.Asn1;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using static ChatroomWithRabbitMq.Service.Chatroom.ChatroomService;

namespace ChatroomWithRabbitMq.Service.StockBot
{
    public class StockBotService : IStockBotService
    {
        public StockBotService() { }

        public bool GetStock(string StockCode)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://stooq.com/q/l/?s=" + StockCode.Substring(7) + "&f=sd2t2ohlcv&h&e=csv");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using var reader = new StreamReader(resp.GetResponseStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                // Read CSV file
                var stock = csv.GetRecords<Stock>().FirstOrDefault();
                
                if (stock != null && stock.Close != "N/D")
                {
                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();
                    channel.QueueDeclare(queue: "Message2", durable: false, exclusive: false, autoDelete: false,
                        arguments: null);

                    var body = Encoding.UTF8.GetBytes("“" + stock.Symbol + " quote is "+ stock.Close+ " per share”");

                    channel.BasicPublish(exchange: "", routingKey: "Message2", basicProperties: null, body: body);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
