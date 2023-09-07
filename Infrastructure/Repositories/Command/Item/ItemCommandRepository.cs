namespace Domain.Repositories.Command;

public class ItemCommandRepository : GenericRepository<Item>, IItemCommandRepository
{
    public ItemCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(Item entity)
    {
        QueryString = $@"INSERT INTO Item
                            (ItemName,
                            ItemDesc,
                            Ammount,
                            Periodity,
                            StartDate,
                            EndDate,
                            Cancelled,
                            CategoryId,
                            SubCategoryId,
                            ItemTypeId,
                            AmmountTypeId,
                            PeriodTypeId,
                            UserId,
                            AccountId)
                        OUTPUT INSERTED.ItemId
                        VALUES
                            (@ItemName,
                            @ItemDesc,
                            @Ammount,
                            @Periodity,
                            @StartDate,
                            @EndDate,
                            @Cancelled,
                            @CategoryId,
                            @SubCategoryId,
                            @ItemTypeId,
                            @AmmountTypeId,
                            @PeriodTypeId,
                            @UserId,
                            @AccountId)";

        var result = await ExecuteScalarAsync(entity);
        return result;
    }
    public async Task<int> Update(Item entity)
    {
        QueryString = $@"UPDATE Item SET
                            ItemName = @ItemName,
                            ItemDesc = @ItemDesc,
                            Ammount = @Ammount,
                            Periodity = @Periodity,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            Cancelled = @Cancelled,
                            CategoryId = @CategoryId,
                            SubCategoryId = @SubCategoryId,
                            ItemTypeId = @ItemTypeId,
                            AmmountTypeId = @AmmountTypeId,
                            PeriodTypeId = @PeriodTypeId,
                            UserId = @UserId,
                            AccountId = @AccountId
                        WHERE ItemId = @ItemId";

        var result = await ExecuteAsync(entity);
        return result;
    }
}
