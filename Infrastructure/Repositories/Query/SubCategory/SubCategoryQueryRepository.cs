namespace Infrastructure.Repositories.Query;

public class SubCategoryQueryRepository : GenericRepository<SubCategoryWithCategory>, ISubCategoryQueryRepository
{
    private readonly string TABLE = "SubCategory";

    public SubCategoryQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<List<SubCategoryWithCategory>> FindByUserId(int userId)
    {
        Param = new { UserId = userId };
        QueryString = $@"SELECT a.*, b.CategoryDesc 
                        FROM {TABLE} a 
                        LEFT JOIN Category b on a.CategoryId = b.CategoryId
                        WHERE b.userId = @UserId";
        var result = await FindAsync();
        return result.ToList();
    }

    public async Task<SubCategoryWithCategory> FindById(int subCategoryId)
    {
        Param = new { SubCategoryId = subCategoryId };
        QueryString = $@"SELECT a.*, b.CategoryDesc 
                        FROM {TABLE} a
                        LEFT JOIN Category b on a.CategoryId = b.CategoryId
                        WHERE SubCategoryId = @SubCategoryId";
        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
