namespace Domain.Repositories.Query;

public interface IItemSummaryQueryRepository
{
    Task<List<ItemSummary>> FindByUserId(int userId);
}
