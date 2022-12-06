using System;
using System.Threading.Tasks;

namespace Giftshop.Notifications.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, 
                    "AuthenticatedUsersGroup");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, 
                    "AuthenticatedUsersGroup");
            }
        }
    }
}
