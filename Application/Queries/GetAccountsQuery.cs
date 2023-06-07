namespace Application.Queries;

public class GetAccountsQuery : IRequest<List<AccountResponse>>
{
    public int UserId { get; set; }

    public GetAccountsQuery(int userId)
    {
        UserId = userId;
    }
}
