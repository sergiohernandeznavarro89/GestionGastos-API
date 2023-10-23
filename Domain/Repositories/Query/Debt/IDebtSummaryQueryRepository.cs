namespace Domain.Repositories.Query;

public interface IDebtSummaryQueryRepository
{
    Task<List<DebtSummary>> FindByUserId(int userId);
}
