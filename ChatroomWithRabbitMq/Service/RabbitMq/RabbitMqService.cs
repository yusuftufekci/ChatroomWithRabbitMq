using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace ChatroomWithRabbitMq.Service.RabbitMq
{
    public class RabbitMqService
    {
        private readonly string _hostName = "localhost";

        public IConnection GetRabbitMqConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };

            return connectionFactory.CreateConnection();
        }
    }
}
