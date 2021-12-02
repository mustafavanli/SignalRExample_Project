using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRExample_Project.Hubs;
using System.Threading.Tasks;

namespace SignalRExample_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        ChatHub _hub;
        public ChatController(ChatHub hub)
        {
            _hub = hub;
        }

        [HttpPost("Notification")]
        public async Task<IActionResult> PostNotification(string message)
        {
            await _hub.Clients.All.SendAsync("Notification",message);
            return Ok();
        }
    }
}
