namespace Domain.Repositories.Query;

public interface IItemPaymentQueryRepository
{
    Task<ItemPayment> FindByItemAndThisMonth(int itemId);
}
