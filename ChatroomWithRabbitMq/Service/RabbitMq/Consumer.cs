using ChatroomWithRabbitMq.Core.StockBot;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ChatroomWithRabbitMq.Service.RabbitMq
{
    public class Consumer : Hub
    {
        private readonly RabbitMqService _rabbitMqService;


        public Consumer(string queueName)
        {
            _rabbitMqService = new RabbitMqService();


            using (var connection = _rabbitMqService.GetRabbitMqConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    // Received event listen all the time
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();

                        var message = Encoding.UTF8.GetString(body);
                        if(message != null)
                        {
                            Clients.All.SendAsync("receiveMessage", message);
                            Console.WriteLine(message);
                        }
                        //_chatroomHub.SendStockMessage(message);
                    };
                    
                    channel.BasicConsume(queueName, true, consumer);
                }
            }
        }
    }
}
