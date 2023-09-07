namespace Domain.Repositories.Command;

public interface ISubCategoryCommandRepository
{
    Task<int> Add(SubCategory entity);
    Task<int> Delete(SubCategory entity);
    Task<int> Update(SubCategory entity);
}
