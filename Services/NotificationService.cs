#nullable enable
using Catalog_Medical.Hubs;
using Catalog_Medical.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Catalog_Medical.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
    }
}