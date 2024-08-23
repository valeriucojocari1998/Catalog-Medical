using Microsoft.AspNetCore.SignalR;

namespace Catalog_Medical.Hubs;

public class NotificationHub : Hub
{
    // Your methods here
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
