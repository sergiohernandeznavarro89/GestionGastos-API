namespace Application.Mappers;

public class UserMapper : Profile
{
    public UserMapper() 
    {
        CreateMap<UserResponse, User>().ReverseMap();
    }
}
