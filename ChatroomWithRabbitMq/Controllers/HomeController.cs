using ChatroomWithRabbitMq.Core.Service;
using ChatroomWithRabbitMq.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatroomWithRabbitMq.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatroomService _chatroomService;
        public HomeController(IChatroomService chatroomService)
        {
            _chatroomService = chatroomService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Chatroom()
        {          
            return View(await _chatroomService.GetMessages());            
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Message message)
        {
            await _chatroomService.CreateMessages(User, message);
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}