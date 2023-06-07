namespace Application.Commands;

public class AddAccountCommand : IRequest<AddAccountResponse>
{
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public int UserId { get; set; }
    public decimal Ammount { get; set; }
}
