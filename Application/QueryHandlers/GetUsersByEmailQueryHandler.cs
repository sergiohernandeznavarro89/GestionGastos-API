namespace Application.QueryHandlers;

public class GetUsersByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserResponse>
{

    private readonly IMapper _mapper;
    private readonly IUserQueryRepository _userQueryRepository;

    public GetUsersByEmailQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
    {
        _mapper = mapper;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<UserResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var response = new UserResponse();
        response = _mapper.Map<UserResponse>(await _userQueryRepository.FindByEmail(request.Email));
        
        return response;
    }
}
