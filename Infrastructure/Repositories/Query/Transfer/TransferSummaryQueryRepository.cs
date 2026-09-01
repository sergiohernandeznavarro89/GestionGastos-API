using Domain.Entities;
using Domain.Repositories.Query;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Repositories.Query;

public class TransferSummaryQueryRepository : GenericRepository<TransferSummary>, ITransferSummaryQueryRepository
{    
    public TransferSummaryQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {        
    }
    
    public async Task<List<TransferSummary>> FindByUserId(int userId)
    {
        Param = new { UserId = userId };
        QueryString = $@"SELECT t.*, 
                                ao.AccountName as OriginAccountName, 
                                ad.AccountName as DestinationAccountName, 
                                c.CategoryDesc, 
                                sc.SubCategoryDesc, 
                                pt.PeriodTypeDesc
                        FROM Transfer t
                        LEFT JOIN Account ao on t.OriginAccountId = ao.AccountId
                        LEFT JOIN Account ad on t.DestinationAccountId = ad.AccountId
                        LEFT JOIN Category c on t.CategoryId = c.CategoryId
                        LEFT JOIN SubCategory sc on t.SubCategoryId = sc.SubCategoryId
                        LEFT JOIN PeriodType pt on t.PeriodTypeId = pt.PeriodTypeId
                        WHERE t.UserId = @UserId AND t.Cancelled = 0";

        var result = await FindAsync();
        return result.ToList();
    }
}
