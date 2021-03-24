using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBox.Models.HistoryChat;
using Microsoft.AspNetCore.Mvc;

namespace ChatBox.Controllers
{
    public class ChatBotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChatPreview(int UserID,int ReceiveID)
        {
            ChatHistory ChatHistory = new ChatHistory();

            return View(ChatHistory);

        }
    }
   
}
