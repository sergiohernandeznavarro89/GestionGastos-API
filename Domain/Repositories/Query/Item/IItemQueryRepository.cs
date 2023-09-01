namespace Domain.Repositories.Query;

public interface IItemQueryRepository
{
    Task<Item> FindById(int itemId);
}
