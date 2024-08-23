namespace Catalog_Medical.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(string message);
}
