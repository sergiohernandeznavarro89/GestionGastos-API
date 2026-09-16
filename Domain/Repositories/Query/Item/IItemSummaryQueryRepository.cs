namespace Domain.Repositories.Query;

public interface IItemSummaryQueryRepository
{
    Task<List<ItemSummary>> FindByUserId(int userId);
    Task<List<ItemSummary>> FindExecutedByMonth(int userId, int monthsOffset);
}
