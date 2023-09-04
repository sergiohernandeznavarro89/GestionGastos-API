namespace Domain.Repositories.Query;

public interface ISubCategoryQueryRepository
{
    Task<List<SubCategoryWithCategory>> FindByUserId(int userId);
    Task<SubCategoryWithCategory> FindById(int subCategoryId);
}
