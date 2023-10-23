namespace Infrastructure.Repositories.Query;

public class DebtSummaryQueryRepository : GenericRepository<DebtSummary>, IDebtSummaryQueryRepository
{    
    public DebtSummaryQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<List<DebtSummary>> FindByUserId(int userId)
    {
        Param = new { UserId = userId };
        QueryString = $@"SELECT d.*, c.CategoryDesc, sc.SubCategoryDesc, dt.DebtTypeDesc
                        FROM Debt d                        
                        LEFT JOIN Category c on d.CategoryId = c.CategoryId
                        LEFT JOIN SubCategory sc on d.SubCategoryId = sc.SubCategoryId
                        LEFT JOIN DebtType dt on d.DebtTypeId = dt.DebtTypeId                                                
                        WHERE d.UserId = @UserId";

        var result = await FindAsync();
        return result.ToList();
    }
}
