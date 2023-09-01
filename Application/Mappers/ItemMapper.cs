namespace Application.Mappers;

public class ItemMapper : Profile
{
    public ItemMapper() 
    {        
        CreateMap<AddItemCommand, Item>();
        CreateMap<ItemSummary, PendingPayItemResponse>();
    }
}
