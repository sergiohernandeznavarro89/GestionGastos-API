using Domain.Entities;

namespace Domain.Repositories.Command;

public class SubCategoryCommandRepository : GenericRepository<SubCategory>, ISubCategoryCommandRepository
{
    public SubCategoryCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(SubCategory entity)
    {
        QueryString = $@"INSERT INTO SubCategory
                            (SubCategoryDesc,
                            CategoryId)
                        OUTPUT INSERTED.SubCategoryId
                        VALUES
                            (@SubCategoryDesc,
                            @CategoryId)";
        var result = await ExecuteScalarAsync(entity);
        return result;
    }

    public async Task<int> Update(SubCategory entity)
    {
        QueryString = $@"UPDATE SubCategory SET
                            SubCategoryDesc = @SubCategoryDesc,
                            CategoryId = @CategoryId
                        WHERE SubCategoryId = @SubCategoryId";

        var result = await ExecuteAsync(entity);
        return result;
    }

    public async Task<int> Delete(SubCategory entity)
    {
        Param = new { entity.SubCategoryId };
        QueryString = $@"DELETE FROM SubCategory WHERE SubCategoryId = @SubCategoryId";
        var result = await ExecuteAsync();
        return result;
    }
}
