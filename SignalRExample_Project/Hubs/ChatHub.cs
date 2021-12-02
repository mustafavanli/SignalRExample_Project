using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRExample_Project.Hubs
{
    public class ChatHub:Hub
    {
        static HashSet<string> users = new HashSet<string>();
        public async override Task OnConnectedAsync()
        {
            users.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public async Task SendMessageAsync(string message)
        {
            if (users.Contains(Context.ConnectionId))
            {
                await Clients.All.SendAsync("receiveMessage", Context.ConnectionId + ":" + message);
            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            users.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
