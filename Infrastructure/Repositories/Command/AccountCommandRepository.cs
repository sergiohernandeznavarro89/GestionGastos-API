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
}
