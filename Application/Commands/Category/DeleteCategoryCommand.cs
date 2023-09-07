namespace Application.Commands;

public class DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
{
    public int CategoryId { get; set; }

    public DeleteCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }
}
