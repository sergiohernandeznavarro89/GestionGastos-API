using Domain.Entities;
using Domain.Repositories.Command;
using Domain.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Command;

public class TransferCommandRepository : GenericRepository<Transfer>, ITransferCommandRepository
{
    public TransferCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(Transfer entity)
    {
        QueryString = $@"INSERT INTO Transfer
                            (TransferName,
                             TransferDesc,
                             Ammount,
                             Periodity,
                             StartDate,
                             EndDate,
                             Cancelled,
                             CategoryId,
                             SubCategoryId,
                             PeriodTypeId,
                             UserId,
                             OriginAccountId,
                             DestinationAccountId)
                        OUTPUT INSERTED.TransferId
                        VALUES
                            (@TransferName,
                             @TransferDesc,
                             @Ammount,
                             @Periodity,
                             @StartDate,
                             @EndDate,
                             @Cancelled,
                             @CategoryId,
                             @SubCategoryId,
                             @PeriodTypeId,
                             @UserId,
                             @OriginAccountId,
                             @DestinationAccountId)";

        var result = await ExecuteScalarAsync(entity);
        return result;
    }

    public async Task<int> Update(Transfer entity)
    {
        QueryString = $@"UPDATE Transfer SET
                            TransferName = @TransferName,
                            TransferDesc = @TransferDesc,
                            Ammount = @Ammount,
                            Periodity = @Periodity,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            Cancelled = @Cancelled,
                            CategoryId = @CategoryId,
                            SubCategoryId = @SubCategoryId,
                            PeriodTypeId = @PeriodTypeId,
                            UserId = @UserId,
                            OriginAccountId = @OriginAccountId,
                            DestinationAccountId = @DestinationAccountId
                        WHERE TransferId = @TransferId";

        var result = await ExecuteAsync(entity);
        return result;
    }

    public async Task<int> Delete(int transferId)
    {
        QueryString = $@"DELETE FROM Transfer WHERE TransferId = @TransferId";
        var param = new { TransferId = transferId };
        
        Param = param;
        var result = await ExecuteAsync();
        return result;
    }
}
