namespace Application.Queries;

public class GetSubCategoriesQuery : IRequest<List<SubCategoryResponse>>
{
    public int UserId { get; set; }

    public GetSubCategoriesQuery(int userId)
    {
        UserId = userId;
    }
}
