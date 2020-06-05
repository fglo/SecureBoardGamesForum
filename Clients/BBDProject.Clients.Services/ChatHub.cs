using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BBDProject.Clients.Services
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public Task SendNewMessagesMessage()
        {
            return Clients.All.SendAsync("NewMessages");
        }
    }
}
