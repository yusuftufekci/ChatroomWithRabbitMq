using ChatroomWithRabbitMq.Models;
using ChatroomWithRabbitMq.Models.Dto;

namespace ChatroomWithRabbitMq.Core.StockBot
{
    public interface IStockBotService
    {
        bool GetStock(string StockCode);

    }
}
