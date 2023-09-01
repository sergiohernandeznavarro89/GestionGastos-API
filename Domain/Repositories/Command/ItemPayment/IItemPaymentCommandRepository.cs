namespace Domain.Repositories.Command;

public interface IItemPaymentCommandRepository
{
    Task<int> Add(ItemPayment entity);
}
