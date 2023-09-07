namespace Application.Mappers;

public class ItemMapper : Profile
{
    public ItemMapper() 
    {        
        CreateMap<AddItemCommand, Item>();
        CreateMap<ItemSummary, PendingPayItemResponse>();
        CreateMap<ItemSummary, ItemResponse>();
        CreateMap<ItemSummary, NextMonthPendingPayItemResponse>();
        CreateMap<UpdateItemCommand, Item>();
    }
}
