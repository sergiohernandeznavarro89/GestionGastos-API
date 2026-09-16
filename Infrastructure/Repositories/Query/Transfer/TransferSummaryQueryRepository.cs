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
                        WHERE t.UserId = @UserId AND t.Cancelled = false";

        var result = await FindAsync();
        return result.ToList();
    }

    public async Task<List<TransferSummary>> FindExecutedByMonth(int userId, int monthsOffset)
    {
        System.DateTime targetDate = System.DateTime.Now.AddMonths(-monthsOffset);
        Param = new { UserId = userId, TargetMonth = targetDate.Month, TargetYear = targetDate.Year };
        QueryString = $@"SELECT t.TransferId, t.TransferName, t.TransferDesc, 
                               COALESCE(tp.Ammount, t.Ammount) AS Ammount, 
                               t.Periodity, 
                               COALESCE(tp.PaymentDate, t.StartDate) AS StartDate, 
                               t.EndDate, t.Cancelled, t.CategoryId, t.SubCategoryId, t.PeriodTypeId, t.UserId,
                               t.OriginAccountId, t.DestinationAccountId,
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
                        LEFT JOIN TransferPayment tp ON t.TransferId = tp.TransferId AND EXTRACT(MONTH FROM tp.PaymentDate) = @TargetMonth AND EXTRACT(YEAR FROM tp.PaymentDate) = @TargetYear
                        WHERE t.UserId = @UserId AND t.Cancelled = false
                          AND (
                              (t.PeriodTypeId = 1 AND EXTRACT(MONTH FROM t.StartDate) = @TargetMonth AND EXTRACT(YEAR FROM t.StartDate) = @TargetYear)
                              OR 
                              (t.PeriodTypeId = 2 AND tp.TransferPaymentId IS NOT NULL)
                          )";

        var result = await FindAsync();
        return result.ToList();
    }
}
