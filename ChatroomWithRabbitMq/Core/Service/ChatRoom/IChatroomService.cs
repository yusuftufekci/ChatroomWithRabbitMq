using ChatroomWithRabbitMq.Models;
using System.Security.Claims;

namespace ChatroomWithRabbitMq.Core.Service.ChatRoom
{
    public interface IChatroomService
    {
        Task<List<Message>> GetMessages();
        Task CreateMessages(ClaimsPrincipal User, Message message);
    }
}
