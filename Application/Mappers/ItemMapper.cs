namespace Application.Mappers;

public class ItemMapper : Profile
{
    public ItemMapper() 
    {
        CreateMap<AddItemCommand, Item>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)));
        CreateMap<ItemSummary, PendingPayItemResponse>();
        CreateMap<ItemSummary, ItemResponse>();
        CreateMap<ItemSummary, NextMonthPendingPayItemResponse>();
        CreateMap<UpdateItemCommand, Item>();
    }
}
