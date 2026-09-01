using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories.Query;

public interface ITransferQueryRepository
{
    Task<Transfer> FindById(int transferId);
}
