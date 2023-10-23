namespace Application.Queries;

public class GetDebtQuery : IRequest<List<DebtResponse>>
{
    public int UserId { get; set; }

    public GetDebtQuery(int userId)
    {
        UserId = userId;
    }
}
