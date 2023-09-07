namespace Application.Commands;

public class UpdateSubCategoryCommand : IRequest<UpdateSubCategoryResponse>
{
    public int SubCategoryId { get; set; }
    public string SubCategoryDesc { get; set; }
}
