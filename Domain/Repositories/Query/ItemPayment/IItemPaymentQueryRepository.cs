namespace Domain.Repositories.Query;

public interface IItemPaymentQueryRepository
{
    Task<ItemPayment> FindByItemAndThisMonth(int itemId);
    Task<ItemPayment> FindByItemAndNextMonth(int itemId);
}
