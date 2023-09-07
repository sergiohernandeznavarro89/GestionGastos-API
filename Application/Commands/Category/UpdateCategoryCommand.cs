namespace Application.Commands;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    public int CategoryId { get; set; }
    public string CategoryDesc { get; set; }
}
