namespace Application.Queries;

public class GetNextMonthPendingPayItemQuery : IRequest<List<NextMonthPendingPayItemResponse>>
{
    public int UserId { get; set; }

    public GetNextMonthPendingPayItemQuery(int userId)
    {
        UserId = userId;
    }
}
