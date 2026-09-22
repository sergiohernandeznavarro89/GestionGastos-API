namespace Infrastructure.Repositories.Query;

public class ItemPaymentQueryRepository : GenericRepository<ItemPayment>, IItemPaymentQueryRepository
{    
    public ItemPaymentQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }

    public async Task<ItemPayment> FindByItemAndThisMonth(int itemId)
    {
        Param = new { ItemId = itemId };
        QueryString = $@"SELECT *
                        FROM ItemPayment
                        WHERE ItemId = @ItemId
                            AND EXTRACT(MONTH FROM PaymentDate) = EXTRACT(MONTH FROM CURRENT_TIMESTAMP)
                            AND EXTRACT(YEAR FROM PaymentDate) = EXTRACT(YEAR FROM CURRENT_TIMESTAMP);";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
    
    public async Task<ItemPayment> FindByItemAndNextMonth(int itemId)
    {
        Param = new { ItemId = itemId };
        QueryString = $@"SELECT *
                        FROM ItemPayment
                        WHERE ItemId = @ItemId
                            AND EXTRACT(MONTH FROM PaymentDate) = EXTRACT(MONTH FROM (CURRENT_TIMESTAMP + INTERVAL '1 month'))
                            AND EXTRACT(YEAR FROM PaymentDate) = EXTRACT(YEAR FROM (CURRENT_TIMESTAMP + INTERVAL '1 month'));";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
