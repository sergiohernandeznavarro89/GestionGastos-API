namespace Domain.Repositories.Query;

public interface ICategoryQueryRepository
{
    Task<List<Category>> FindByUserId(int userId);
    Task<Category> FindById(int categoryId);
}
