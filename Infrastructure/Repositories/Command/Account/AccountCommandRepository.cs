using Domain.Entities;

namespace Domain.Repositories.Command;

public class AccountCommandRepository : GenericRepository<Account>, IAccountCommandRepository
{
    public AccountCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(Account entity)
    {
        QueryString = $@"INSERT INTO Account
                            (AccountName,
                            UserId,
                            Ammount)
                        OUTPUT INSERTED.AccountId
                        VALUES
                            (@AccountName,
                            @UserId,
                            @Ammount)";
        var result = await ExecuteScalarAsync(entity);
        return result;
    }

    public async Task<int> Delete(Account entity)
    {
        Param = new { entity.AccountId };
        QueryString = $@"DELETE FROM Account WHERE AccountId = @AccountId";
        var result = await ExecuteAsync();
        return result;
    }
}
