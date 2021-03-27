using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace ChatBox.Hubs
{
    public class ChatHub : Hub
    {
        const int CheckUser = 1;
        public async Task SendMessage(string user, string message,string imgURL)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message, CheckUser, imgURL);
            }

        }
    }
}
