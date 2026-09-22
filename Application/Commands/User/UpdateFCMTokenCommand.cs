namespace Application.Commands;

public class UpdateFCMTokenCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public string FCMToken { get; set; }
}
