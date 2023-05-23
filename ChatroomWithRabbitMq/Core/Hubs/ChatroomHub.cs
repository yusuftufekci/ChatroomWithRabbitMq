using ChatroomWithRabbitMq.Core.StockBot;
using ChatroomWithRabbitMq.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatroomWithRabbitMq.Core.Hubs
{
    public class ChatroomHub : Hub
    {
        private readonly IStockBotService _stockBotService;
        public ChatroomHub(IStockBotService stockBotService)
        {
            _stockBotService= stockBotService;
        }
        public async Task SendMessage(Message message)
        {
            if (message.Text.Contains("/stock="))
            {
                var result = _stockBotService.GetStock(message.Text);
                if (result == false)
                {
                    await Clients.All.SendAsync("receiveMessage", message);
                }
            }
            else
            {
                await Clients.All.SendAsync("receiveMessage", message);
            }
            

        }
        public async Task SendStockMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
