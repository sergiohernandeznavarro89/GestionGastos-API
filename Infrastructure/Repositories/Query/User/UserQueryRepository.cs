namespace Infrastructure.Repositories.Query;

public class UserQueryRepository : GenericRepository<User>, IUserQueryRepository
{
    private readonly string TABLE = "Users";

    public UserQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }

    public async Task<IEnumerable<User>> FindAll()
    {
        QueryString = $"SELECT * FROM {TABLE}";
        var result = await FindAsync();
        return result;
    }
    
    public async Task<User> FindByEmail(string email)
    {
        Param = new { Email = email };
        QueryString = $"SELECT * FROM {TABLE} WHERE userEmail = @Email";
        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
