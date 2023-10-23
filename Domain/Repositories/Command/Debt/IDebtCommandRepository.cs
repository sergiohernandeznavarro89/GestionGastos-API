namespace Domain.Repositories.Command;

public interface IDebtCommandRepository
{
    Task<int> Add(Debt entity);
    //Task<int> Update(Debt entity);
}
