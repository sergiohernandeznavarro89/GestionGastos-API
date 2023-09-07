namespace Application.Commands;

public class DeleteSubCategoryCommand : IRequest<DeleteSubCategoryResponse>
{
    public int SubCategoryId { get; set; }

    public DeleteSubCategoryCommand(int subCategory)
    {
        SubCategoryId = subCategory;
    }
}
