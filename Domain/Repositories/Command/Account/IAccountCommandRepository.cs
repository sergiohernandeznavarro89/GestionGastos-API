namespace Domain.Repositories.Command;

public interface IAccountCommandRepository
{
    Task<int> Add(Account entity);
    Task<int> Delete(Account entity);
}
