using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace ChatBox.Hubs
{
    public class ChatHub : Hub
    {
        const int CheckUser = 2;
        public async Task SendMessage(string userIDReceive, string username, string message,string imgURL)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await Clients.All.SendAsync("ReceiveMessage", userIDReceive, username, message, CheckUser, imgURL);
            }

        }
    }
}
