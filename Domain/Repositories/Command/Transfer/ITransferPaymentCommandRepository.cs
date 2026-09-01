using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories.Command;

public interface ITransferPaymentCommandRepository
{
    Task<int> Add(TransferPayment entity);
    Task<int> Update(TransferPayment entity);
    Task<int> Delete(int transferPaymentId);
    Task<int> DeleteByTransferId(int transferId);
}
