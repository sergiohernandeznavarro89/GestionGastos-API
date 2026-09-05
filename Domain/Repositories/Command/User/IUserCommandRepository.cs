using Domain.Entities;

namespace Domain.Repositories.Command;
using static Domain.Configuration.Sql;

public interface IUserCommandRepository : IGenericRepository<User>
{
    Task<int> Add(User entity);
    Task<int> Delete(User entity);
}
