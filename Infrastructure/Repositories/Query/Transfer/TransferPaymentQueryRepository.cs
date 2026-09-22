using Domain.Entities;
using Domain.Repositories.Query;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Repositories.Query;

public class TransferPaymentQueryRepository : GenericRepository<TransferPayment>, ITransferPaymentQueryRepository
{
    public TransferPaymentQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<List<TransferPayment>> FindByTransferId(int transferId)
    {
        Param = new { TransferId = transferId };
        QueryString = $@"SELECT * FROM TransferPayment WHERE TransferId = @TransferId";

        var result = await FindAsync();
        return result.ToList();
    }

    public async Task<TransferPayment> FindByTransferAndThisMonth(int transferId)
    {
        Param = new { TransferId = transferId };
        QueryString = $@"SELECT *
                        FROM TransferPayment
                        WHERE TransferId = @TransferId
                            AND EXTRACT(MONTH FROM PaymentDate) = EXTRACT(MONTH FROM CURRENT_TIMESTAMP)
                            AND EXTRACT(YEAR FROM PaymentDate) = EXTRACT(YEAR FROM CURRENT_TIMESTAMP);";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
    
    public async Task<TransferPayment> FindByTransferAndNextMonth(int transferId)
    {
        Param = new { TransferId = transferId };
        QueryString = $@"SELECT *
                        FROM TransferPayment
                        WHERE TransferId = @TransferId
                            AND EXTRACT(MONTH FROM PaymentDate) = EXTRACT(MONTH FROM (CURRENT_TIMESTAMP + INTERVAL '1 month'))
                            AND EXTRACT(YEAR FROM PaymentDate) = EXTRACT(YEAR FROM (CURRENT_TIMESTAMP + INTERVAL '1 month'));";

        var result = await FindFirstOrDefaultAsync();
        return result;
    }
}
