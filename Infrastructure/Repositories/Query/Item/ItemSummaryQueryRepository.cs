namespace Infrastructure.Repositories.Query;

public class ItemSummaryQueryRepository : GenericRepository<ItemSummary>, IItemSummaryQueryRepository
{    
    public ItemSummaryQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<List<ItemSummary>> FindByUserId(int userId)
    {
        Param = new { UserId = userId };
        QueryString = $@"SELECT i.*, a.AccountName, c.CategoryDesc, sc.SubCategoryDesc, it.ItemTypeDesc, at.AmmountTypeDesc, pt.PeriodTypeDesc
                        FROM Item i
                        LEFT JOIN Account a on i.AccountId = a.AccountId
                        LEFT JOIN Category c on i.CategoryId = c.CategoryId
                        LEFT JOIN SubCategory sc on i.SubCategoryId = sc.SubCategoryId
                        LEFT JOIN ItemType it on i.ItemTypeId = it.ItemTypeId
                        LEFT JOIN AmmountType at on i.AmmountTypeId = at.AmmountTypeId
                        LEFT JOIN PeriodType pt on i.PeriodTypeId = pt.PeriodTypeId
                        WHERE a.UserId = @UserId AND i.Cancelled = 0";

        var result = await FindAsync();
        return result.ToList();
    }
}
