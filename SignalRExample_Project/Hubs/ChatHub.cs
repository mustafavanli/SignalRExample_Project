using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRExample_Project.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserDto> users = new List<UserDto>();
        public async override Task OnConnectedAsync()
        {
            users.Add(new UserDto
            {
                connectionId = Context.ConnectionId,
                userId = Guid.NewGuid().ToString()
            });

            
            await base.OnConnectedAsync();
            // UserList Send
            await Clients.Client(Context.ConnectionId).SendAsync("userList", users);
            //
        }
        public async Task SendMessageAsync(string userId, string message)
        {
            if (users.Select(x => x.userId).Contains(userId))
            {
                var user = users.Where(x => x.userId == userId).FirstOrDefault();
                if (user != null)
                {
                    await Clients.Client(user.connectionId).SendAsync("receiveMessage", message);
                }

            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = users.Where(x => x.connectionId == Context.ConnectionId).FirstOrDefault();
            if(user != null)
            {
                users.Remove(user);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class UserDto
    {
        public string userId { get; set; }
        public string connectionId { get; set; }
    }
}
