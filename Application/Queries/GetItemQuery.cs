namespace Application.Queries;

public class GetItemQuery : IRequest<List<ItemResponse>>
{
    public int UserId { get; set; }

    public GetItemQuery(int userId)
    {
        UserId = userId;
    }
}
