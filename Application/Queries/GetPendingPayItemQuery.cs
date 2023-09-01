namespace Application.Queries;

public class GetPendingPayItemQuery : IRequest<List<PendingPayItemResponse>>
{
    public int UserId { get; set; }

    public GetPendingPayItemQuery(int userId)
    {
        UserId = userId;
    }
}
