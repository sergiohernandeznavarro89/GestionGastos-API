namespace Domain.Repositories.Command;

public class ItemPaymentCommandRepository : GenericRepository<ItemPayment>, IItemPaymentCommandRepository
{
    public ItemPaymentCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(ItemPayment entity)
    {
        QueryString = $@"INSERT INTO ItemPayment
                            (ItemId,
                            PaymentDate,
                            Ammount)
                        
                        VALUES
                            (@ItemId,
                            @PaymentDate,
                            @Ammount) RETURNING ItemPaymentId";

        var result = await ExecuteScalarAsync(entity);
        return result;
    }
}
