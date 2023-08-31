namespace Application.Commands;

public class DeleteAccountCommand : IRequest<DeleteAccountResponse>
{
    public int AccountId { get; set; }

    public DeleteAccountCommand(int accountId)
    {
        AccountId = accountId;
    }
}
