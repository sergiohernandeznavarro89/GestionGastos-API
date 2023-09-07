namespace Application.Commands;

public class UpdateAccountCommand : IRequest<UpdateAccountResponse>
{
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public decimal Ammount { get; set; }
}
