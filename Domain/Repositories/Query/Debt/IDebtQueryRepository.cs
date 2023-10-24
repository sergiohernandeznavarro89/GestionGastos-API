namespace Domain.Repositories.Query;

public interface IDebtQueryRepository
{
    Task<Debt> FindById(int debtId);
}
