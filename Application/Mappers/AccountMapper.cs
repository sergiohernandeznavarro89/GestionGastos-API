namespace Application.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper() 
    {
        CreateMap<Account, AccountResponse>();
        CreateMap<AddAccountCommand, Account>();
        CreateMap<UpdateAccountCommand, Account>();
    }
}
