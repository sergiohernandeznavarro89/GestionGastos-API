using Domain.Entities;
using Domain.Repositories.Command;
using static Domain.Configuration.Sql;

namespace Infrastructure.Repositories.Command.User;

public class UserCommandRepository : GenericRepository<Domain.Entities.User>, IUserCommandRepository
{
    public UserCommandRepository(IDatabaseConnection DbConnection) : base(DbConnection)
    {
    }

    public async Task<int> Add(Domain.Entities.User entity)
    {
        QueryString = $@"INSERT INTO Users
                            (UserName,
                             UserLastName,
                             UserPass,
                             UserEmail)
                        OUTPUT INSERTED.UserId
                        VALUES
                            (@UserName,
                             @UserLastName,
                             @UserPass,
                             @UserEmail)";
        var result = await ExecuteScalarAsync(entity);
        return result;
    }

    public async Task<int> Delete(Domain.Entities.User entity)
    {
        Param = new { entity.UserId };
        QueryString = $@"DELETE FROM Users WHERE UserId = @UserId";
        var result = await ExecuteAsync();
        return result;
    }
}
