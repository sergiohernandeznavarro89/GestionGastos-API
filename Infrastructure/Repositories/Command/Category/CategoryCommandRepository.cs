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
                        OUTPUT INSERTED.CategoryId
                        VALUES
                            (@CategoryDesc,
                            @UserId)";
        var result = await ExecuteScalarAsync(entity);
        return result;
    }
    
    //public async Task<int> Update(Account entity)
    //{
    //    QueryString = $@"UPDATE Account SET
    //                        AccountName = @AccountName,
    //                        Ammount = @Ammount
    //                    WHERE AccountId = @AccountId";

    //    var result = await ExecuteAsync(entity);
    //    return result;
    //}

    //public async Task<int> Delete(Account entity)
    //{
    //    Param = new { entity.AccountId };
    //    QueryString = $@"DELETE FROM Account WHERE AccountId = @AccountId";
    //    var result = await ExecuteAsync();
    //    return result;
    //}
}
