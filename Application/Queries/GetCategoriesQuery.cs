namespace Application.Queries;

public class GetCategoriesQuery : IRequest<List<CategoryResponse>>
{
    public int UserId { get; set; }

    public GetCategoriesQuery(int userId)
    {
        UserId = userId;
    }
}
