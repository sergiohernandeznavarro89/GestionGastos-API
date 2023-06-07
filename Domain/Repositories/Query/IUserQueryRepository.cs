namespace Domain.Repositories.Query;

public interface IUserQueryRepository
{
    Task<IEnumerable<User>> FindAll();
    Task<User> FindByEmail(string email);
}
