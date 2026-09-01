using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories.Command;

public interface ITransferCommandRepository
{
    Task<int> Add(Transfer entity);
    Task<int> Update(Transfer entity);
    Task<int> Delete(int transferId);
}
