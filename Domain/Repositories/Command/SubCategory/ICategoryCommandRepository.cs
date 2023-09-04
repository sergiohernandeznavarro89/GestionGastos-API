namespace Domain.Repositories.Command;

public interface ISubCategoryCommandRepository
{
    Task<int> Add(SubCategory entity);
    //Task<int> Delete(Category entity);
    //Task<int> Update(Category entity);
}
