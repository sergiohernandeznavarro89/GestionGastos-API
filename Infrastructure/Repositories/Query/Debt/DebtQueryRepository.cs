namespace Infrastructure.Repositories.Query;

public class DebtQueryRepository : GenericRepository<Debt>, IDebtQueryRepository
{    
    public DebtQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<Debt> FindById(int debtId)
    {
        Param = new { DebtId = debtId };
        QueryString = $@"SELECT *
                        FROM Debt 
                        WHERE DebtId = @DebtId";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
