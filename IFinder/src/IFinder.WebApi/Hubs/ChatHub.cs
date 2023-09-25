using Microsoft.AspNetCore.SignalR;

namespace IFinder.WebApi.Hubs
{
    public sealed class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
        
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("",message);
        }
    }
}
