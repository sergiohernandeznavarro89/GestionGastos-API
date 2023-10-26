namespace Application.Commands;

public class AddDebtPaymentCommand : IRequest<AddDebtPaymentResponse>
{
    public int DebtId { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }

    public AddDebtPaymentCommand(int debtId, decimal amount, int accountId)
    {
        DebtId = debtId;
        Amount = amount;
        AccountId = accountId;
    }
}
