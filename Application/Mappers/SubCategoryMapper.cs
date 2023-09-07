namespace Application.Mappers;

public class SubCategoryMapper : Profile
{
    public SubCategoryMapper() 
    {
        CreateMap<SubCategoryWithCategory, SubCategoryResponse>();
        CreateMap<AddSubCategoryCommand, SubCategory>();
        CreateMap<UpdateSubCategoryCommand, SubCategory>();
        CreateMap<SubCategoryWithCategory, SubCategory>();
    }
}
