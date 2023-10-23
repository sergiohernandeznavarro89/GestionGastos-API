namespace Application.Mappers;

public class DebtMapper : Profile
{
    public DebtMapper() 
    {
        CreateMap<AddDebtCommand, Debt>();
        CreateMap<DebtSummary, DebtResponse>();
    }
}
