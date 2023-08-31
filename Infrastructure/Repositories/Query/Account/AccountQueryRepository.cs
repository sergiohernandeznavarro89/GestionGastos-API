namespace Infrastructure.Repositories.Query;

public class AccountQueryRepository : GenericRepository<Account>, IAccountQueryRepository
{
    private readonly string TABLE = "Account";

    public AccountQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<List<Account>> FindByUserId(int userId)
    {
        Param = new { UserId = userId };
        QueryString = $"SELECT * FROM {TABLE} WHERE userId = @UserId";
        var result = await FindAsync();
        return result.ToList();
    }

    public async Task<Account> FindById(int accountId)
    {
        Param = new { AccountId = accountId };
        QueryString = $@"SELECT * FROM {TABLE} WHERE accountId = @AccountId";
        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
