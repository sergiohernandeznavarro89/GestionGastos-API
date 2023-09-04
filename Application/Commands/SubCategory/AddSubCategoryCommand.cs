namespace Application.Commands;

public class AddSubCategoryCommand : IRequest<AddSubCategoryResponse>
{
    public int SubCategoryId { get; set; }
    public string SubCategoryDesc { get; set; }
    public int CategoryId { get; set; }    
}
