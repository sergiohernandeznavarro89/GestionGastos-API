using Domain.Entities;

namespace Domain.Repositories.Command;

public class CategoryCommandRepository : GenericRepository<Category>, ICategoryCommandRepository
{
    public CategoryCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(Category entity)
    {
        QueryString = $@"INSERT INTO Category
                            (CategoryDesc,
                            UserId)
                        
                        VALUES
                            (@CategoryDesc,
                            @UserId) RETURNING CategoryId";
        var result = await ExecuteScalarAsync(entity);
        return result;
    }

    public async Task<int> Update(Category entity)
    {
        QueryString = $@"UPDATE Category SET
                            CategoryDesc = @CategoryDesc,
                            UserId = @UserId
                        WHERE CategoryId = @CategoryId";

        var result = await ExecuteAsync(entity);
        return result;
    }

    public async Task<int> Delete(Category entity)
    {
        Param = new { entity.CategoryId };
        QueryString = $@"DELETE FROM Category WHERE CategoryId = @CategoryId";
        var result = await ExecuteAsync();
        return result;
    }
}
