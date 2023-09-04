namespace Application.Mappers;

public class SubCategoryMapper : Profile
{
    public SubCategoryMapper() 
    {
        //CreateMap<SubCategory, SubCategoryResponse>();
        CreateMap<SubCategoryWithCategory, SubCategoryResponse>();
        CreateMap<AddSubCategoryCommand, SubCategory>();
    }
}
