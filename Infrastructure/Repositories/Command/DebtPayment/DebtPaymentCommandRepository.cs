namespace Domain.Repositories.Command;

public class DebtPaymentCommandRepository : GenericRepository<DebtPayment>, IDebtPaymentCommandRepository
{
    public DebtPaymentCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(DebtPayment entity)
    {
        QueryString = $@"INSERT INTO DebtPayment
                            (DebtId,
                            Date,
                            Amount,
                            AccountId)
                        
                        VALUES
                            (@DebtId,
                            @Date,
                            @Amount,
                            @AccountId) RETURNING DebtPaymentId";

        var result = await ExecuteScalarAsync(entity);
        return result;
    }
}
