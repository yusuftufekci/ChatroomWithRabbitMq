using ChatroomWithRabbitMq.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatroomWithRabbitMq.Core.Hubs
{
    public class ChatroomHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("RecieveMessages", message);
        }
    }
}
