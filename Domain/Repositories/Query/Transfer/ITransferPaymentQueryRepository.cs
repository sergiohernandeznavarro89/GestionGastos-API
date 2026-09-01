using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories.Query;

public interface ITransferPaymentQueryRepository
{
    Task<List<TransferPayment>> FindByTransferId(int transferId);
    Task<TransferPayment> FindByTransferAndThisMonth(int transferId);
    Task<TransferPayment> FindByTransferAndNextMonth(int transferId);
}
