using Domain.Entities;
using Domain.Repositories.Command;
using Domain.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Command;

public class TransferPaymentCommandRepository : GenericRepository<TransferPayment>, ITransferPaymentCommandRepository
{
    public TransferPaymentCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(TransferPayment entity)
    {
        QueryString = $@"INSERT INTO TransferPayment
                            (TransferId,
                             PaymentDate,
                             Ammount)
                        OUTPUT INSERTED.TransferPaymentId
                        VALUES
                            (@TransferId,
                             @PaymentDate,
                             @Ammount)";

        var result = await ExecuteScalarAsync(entity);
        return result;
    }

    public async Task<int> Update(TransferPayment entity)
    {
        QueryString = $@"UPDATE TransferPayment SET
                            TransferId = @TransferId,
                            PaymentDate = @PaymentDate,
                            Ammount = @Ammount
                        WHERE TransferPaymentId = @TransferPaymentId";

        var result = await ExecuteAsync(entity);
        return result;
    }

    public async Task<int> Delete(int transferPaymentId)
    {
        QueryString = $@"DELETE FROM TransferPayment WHERE TransferPaymentId = @TransferPaymentId";
        var param = new { TransferPaymentId = transferPaymentId };
        
        Param = param;
        var result = await ExecuteAsync();
        return result;
    }

    public async Task<int> DeleteByTransferId(int transferId)
    {
        QueryString = $@"DELETE FROM TransferPayment WHERE TransferId = @TransferId";
        var param = new { TransferId = transferId };
        
        Param = param;
        var result = await ExecuteAsync();
        return result;
    }
}
