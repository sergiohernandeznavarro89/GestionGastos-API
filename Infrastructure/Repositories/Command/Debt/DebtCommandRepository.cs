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
    public async Task<int> Update(Debt entity)
    {
        QueryString = $@"UPDATE Debt SET
                            DebtName = @DebtName, 
                            StartAmount = @StartAmount,
                            Date = @Date,  
                            CategoryId = @CategoryId,  
                            SubCategoryId = @SubCategoryId,  
                            DebtTypeId = @DebtTypeId,  
                            UserId = @UserId,  
                            DebtorName = @DebtorName,  
                            CurrentAmount = @CurrentAmount
                        WHERE DebtId = @DebtId";

        var result = await ExecuteAsync(entity);
        return result;
    }
}
