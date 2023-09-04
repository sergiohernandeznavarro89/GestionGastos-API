namespace Infrastructure.Repositories.Query;

public class CategoryQueryRepository : GenericRepository<Category>, ICategoryQueryRepository
{
    private readonly string TABLE = "Category";

    public CategoryQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<List<Category>> FindByUserId(int userId)
    {
        Param = new { UserId = userId };
        QueryString = $"SELECT * FROM {TABLE} WHERE userId = @UserId";
        var result = await FindAsync();
        return result.ToList();
    }

    public async Task<Category> FindById(int categoryId)
    {
        Param = new { CategoryId = categoryId};
        QueryString = $@"SELECT * FROM {TABLE} WHERE CategoryId = @CategoryId";
        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
