using Domain.Entities;

namespace Domain.Repositories.Command;

public interface IAccountCommandRepository
{
    Task<int> Add(Account entity);
}
