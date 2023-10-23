namespace Domain.Repositories.Command;

public class DebtCommandRepository : GenericRepository<Debt>, IDebtCommandRepository
{
    public DebtCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(Debt entity)
    {
        QueryString = $@"INSERT INTO Debt (
                            DebtName, 
                            StartAmount, 
                            Date, 
                            CategoryId, 
                            SubCategoryId, 
                            DebtTypeId, 
                            UserId, 
                            DebtorName, 
                            CurrentAmount)
                        VALUES(
                            @DebtName, 
                            @StartAmount, 
                            @Date, 
                            @CategoryId, 
                            @SubCategoryId, 
                            @DebtTypeId, 
                            @UserId, 
                            @DebtorName, 
                            @CurrentAmount
                        )";

        var result = await ExecuteScalarAsync(entity);
        return result;
    }
    //public async Task<int> Update(Item entity)
    //{
    //    QueryString = $@"UPDATE Item SET
    //                        ItemName = @ItemName,
    //                        ItemDesc = @ItemDesc,
    //                        Ammount = @Ammount,
    //                        Periodity = @Periodity,
    //                        StartDate = @StartDate,
    //                        EndDate = @EndDate,
    //                        Cancelled = @Cancelled,
    //                        CategoryId = @CategoryId,
    //                        SubCategoryId = @SubCategoryId,
    //                        ItemTypeId = @ItemTypeId,
    //                        AmmountTypeId = @AmmountTypeId,
    //                        PeriodTypeId = @PeriodTypeId,
    //                        UserId = @UserId,
    //                        AccountId = @AccountId
    //                    WHERE ItemId = @ItemId";

    //    var result = await ExecuteAsync(entity);
    //    return result;
    //}
}
