using ChatroomWithRabbitMq.Data;
using ChatroomWithRabbitMq.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatroomWithRabbitMq.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly UserManager<ChatUser> _userManager;
        public readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, UserManager<ChatUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _context = applicationDbContext;
        }

        public IActionResult Index()
        {
        
            return View();
        }

        public async Task<IActionResult> Chatroom()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.CurrentUserName = currentUser.UserName;

                }
                var messages = await _context.Messages.OrderByDescending(p=>p.Id).Take(50).ToListAsync();
                return View(messages);
            }
            return Redirect("Index");
            
        }

        public async Task<IActionResult> Create(Message message)
        {
           
            message.UserName = User.Identity.Name;
            var sender = await _userManager.GetUserAsync(User);
            message.UserId= sender.Id;
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
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