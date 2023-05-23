using ChatroomWithRabbitMq.Core.Service.ChatRoom;
using ChatroomWithRabbitMq.Data;
using ChatroomWithRabbitMq.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using System.Security.Claims;
using System.Formats.Asn1;
using CsvHelper;
using System.Globalization;

namespace ChatroomWithRabbitMq.Service.Chatroom
{
    public class ChatroomService : IChatroomService
    {
        private readonly ApplicationDbContext _context;
        public readonly UserManager<ChatUser> _userManager;

        public ChatroomService(ApplicationDbContext context, UserManager<ChatUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateMessages(ClaimsPrincipal User, Message message)
        {
            try
            {
                if(message.Text!= null && !message.Text.Contains("/stock="))
                {
                    message.UserName = User.Identity.Name;
                    var sender = await _userManager.GetUserAsync(User);
                    message.UserId = sender.Id;
                    await _context.Messages.AddAsync(message);
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException("Something is wrong",ex);
            }

        }

        public async Task<List<Message>> GetMessages()
        {
            try
            {
                var messages = await _context.Messages.OrderByDescending(p => p.CreateDate).Take(50).ToListAsync();
                return messages;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something is wrong", ex);
            }
        }
        
    }
}
