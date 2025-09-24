using Microsoft.AspNetCore.SignalR;

namespace Broker.Hubs
{
    public class HmiHub : Hub
    {
        public override  async Task OnConnectedAsync()
        {
            var clientId = Context.GetHttpContext()?.Request.Query["clientId"].ToString();
            if (!string.IsNullOrWhiteSpace(clientId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"clientId:{clientId}" );
            }
            await base.OnConnectedAsync();
        }
    }
}
