using Domain.Services;
using FirebaseAdmin.Messaging;

namespace Infrastructure.Services;

public class PushNotificationService : IPushNotificationService
{
    public async Task<bool> SendNotificationAsync(string fcmToken, string title, string body)
    {
        if (string.IsNullOrEmpty(fcmToken)) return false;

        var message = new Message()
        {
            Token = fcmToken,
            Notification = new Notification()
            {
                Title = title,
                Body = body
            }
        };

        try
        {
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return true;
        }
        catch (Exception ex)
        {
            // Logging
            Console.WriteLine($"Error sending push notification: {ex.Message}");
            return false;
        }
    }
}
