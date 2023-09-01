namespace Application.Mappers;

public class ItemPaymentMapper : Profile
{
    public ItemPaymentMapper() 
    {        
        CreateMap<AddItemPaymentCommand, ItemPayment>();        
    }
}
