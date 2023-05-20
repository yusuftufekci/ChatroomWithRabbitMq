using System.ComponentModel.DataAnnotations;

namespace ChatroomWithRabbitMq.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }
        public virtual ChatUser SenderUser { get; set; }
    }
}
