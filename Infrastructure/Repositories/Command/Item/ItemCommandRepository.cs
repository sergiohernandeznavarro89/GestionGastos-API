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
}
