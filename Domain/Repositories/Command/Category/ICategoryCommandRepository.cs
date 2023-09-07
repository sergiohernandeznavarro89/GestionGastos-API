namespace Domain.Repositories.Command;

public interface ICategoryCommandRepository
{
    Task<int> Add(Category entity);
    Task<int> Delete(Category entity);
    Task<int> Update(Category entity);
}
