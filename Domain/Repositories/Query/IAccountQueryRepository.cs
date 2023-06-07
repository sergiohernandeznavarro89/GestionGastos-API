namespace Domain.Repositories.Query;

public interface IAccountQueryRepository
{
    Task<List<Account>> FindByUserId(int userId);
}
