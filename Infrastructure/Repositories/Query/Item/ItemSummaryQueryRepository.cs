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
                        WHERE a.UserId = @UserId AND i.Cancelled = false";

        var result = await FindAsync();
        return result.ToList();
    }

    public async Task<List<ItemSummary>> FindExecutedByMonth(int userId, int monthsOffset)
    {
        DateTime targetDate = DateTime.Now.AddMonths(-monthsOffset);
        Param = new { UserId = userId, TargetMonth = targetDate.Month, TargetYear = targetDate.Year };
        QueryString = $@"SELECT i.ItemId, i.ItemName, i.ItemDesc, 
                               COALESCE(ip.Ammount, i.Ammount) AS Ammount, 
                               i.Periodity, 
                               COALESCE(ip.PaymentDate, i.StartDate) AS StartDate, 
                               i.EndDate, i.Cancelled, i.CategoryId, i.SubCategoryId, i.ItemTypeId, i.AmmountTypeId, i.PeriodTypeId, i.AccountId, i.UserId,
                               a.AccountName, c.CategoryDesc, sc.SubCategoryDesc, it.ItemTypeDesc, at.AmmountTypeDesc, pt.PeriodTypeDesc
                        FROM Item i
                        LEFT JOIN Account a on i.AccountId = a.AccountId
                        LEFT JOIN Category c on i.CategoryId = c.CategoryId
                        LEFT JOIN SubCategory sc on i.SubCategoryId = sc.SubCategoryId
                        LEFT JOIN ItemType it on i.ItemTypeId = it.ItemTypeId
                        LEFT JOIN AmmountType at on i.AmmountTypeId = at.AmmountTypeId
                        LEFT JOIN PeriodType pt on i.PeriodTypeId = pt.PeriodTypeId
                        LEFT JOIN ItemPayment ip ON i.ItemId = ip.ItemId AND EXTRACT(MONTH FROM ip.PaymentDate) = @TargetMonth AND EXTRACT(YEAR FROM ip.PaymentDate) = @TargetYear
                        WHERE a.UserId = @UserId AND i.Cancelled = false
                          AND (
                              (i.PeriodTypeId = 1 AND EXTRACT(MONTH FROM i.StartDate) = @TargetMonth AND EXTRACT(YEAR FROM i.StartDate) = @TargetYear)
                              OR 
                              (i.PeriodTypeId = 2 AND ip.ItemPaymentId IS NOT NULL)
                          )";

        var result = await FindAsync();
        return result.ToList();
    }
}
