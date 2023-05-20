using Microsoft.AspNetCore.Identity;

namespace ChatroomWithRabbitMq.Models
{
    public class ChatUser : IdentityUser
    {
        public ChatUser()
        {
            Messages = new HashSet<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
