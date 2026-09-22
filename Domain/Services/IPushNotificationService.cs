namespace Domain.Services;

public interface IPushNotificationService
{
    Task<bool> SendNotificationAsync(string fcmToken, string title, string body);
}
