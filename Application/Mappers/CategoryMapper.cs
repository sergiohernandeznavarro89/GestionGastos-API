namespace Application.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper() 
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<AddCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
    }
}
