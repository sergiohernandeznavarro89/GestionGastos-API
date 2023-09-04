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
