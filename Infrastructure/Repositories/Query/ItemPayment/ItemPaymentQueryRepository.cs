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
                            AND MONTH(PaymentDate) = MONTH(GETDATE())
                            AND YEAR(PaymentDate) = YEAR(GETDATE());";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
