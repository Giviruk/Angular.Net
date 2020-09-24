using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Models.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly List<Message> _userMessages = new List<Message>();
        
        public Task Send(string message, string nickname)
        {
            _userMessages.Add(new Message(nickname, message));
            return Clients.All.SendAsync("sendMessage", message, nickname);
        }

        public Task GetData()
        {
            return Clients.Caller.SendAsync("getData", _userMessages);
        }
    }
}