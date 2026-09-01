using Domain.Entities;
using Domain.Repositories.Query;
using Domain.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Repositories.Query;

public class TransferQueryRepository : GenericRepository<Transfer>, ITransferQueryRepository
{
    public TransferQueryRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<Transfer> FindById(int transferId)
    {
        Param = new { TransferId = transferId };
        QueryString = $@"SELECT * FROM Transfer WHERE TransferId = @TransferId";

        var result = await FindAsync();
        return result.FirstOrDefault();
    }
}
