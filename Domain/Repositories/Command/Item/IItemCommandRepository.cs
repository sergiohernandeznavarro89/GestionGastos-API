namespace Domain.Repositories.Command;

public interface IItemCommandRepository
{
    Task<int> Add(Item entity);
    Task<int> Update(Item entity);
}
