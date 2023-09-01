namespace Infrastructure.Repositories.Query;

public class ItemQueryRepository : GenericRepository<Item>, IItemQueryRepository
{    
    public ItemQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<Item> FindById(int itemId)
    {
        Param = new { ItemId = itemId };
        QueryString = $@"SELECT *
                        FROM Item 
                        WHERE ItemId = @ItemId";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
