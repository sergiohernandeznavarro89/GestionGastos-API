namespace Domain.Repositories.Query;

public interface IUserQueryRepository
{
    Task<IEnumerable<User>> FindAll();
    Task<User> FindByEmail(string email);
    Task<User> FindById(int userId);
}
