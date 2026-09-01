using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories.Query;

public interface ITransferSummaryQueryRepository
{
    Task<List<TransferSummary>> FindByUserId(int userId);
}
