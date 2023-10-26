namespace Domain.Repositories.Command;

public interface IDebtPaymentCommandRepository
{
    Task<int> Add(DebtPayment entity);
}
