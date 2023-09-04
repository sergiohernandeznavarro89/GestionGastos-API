namespace Application.Commands;

public class AddCategoryCommand : IRequest<AddCategoryResponse>
{
    public int CategoryId { get; set; }
    public string CategoryDesc { get; set; }
    public int UserId { get; set; }    
}
