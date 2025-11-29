using Microsoft.AspNetCore.SignalR;

namespace OnlineExaminationSystem.Hubs
{
    public class notificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {

            var userId = Context.UserIdentifier ?? Context.ConnectionId;
            
            await base.OnConnectedAsync();

        }
    }
}
