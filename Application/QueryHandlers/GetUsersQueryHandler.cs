namespace Application.QueryHandlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
{

    private readonly IMapper _mapper;
    private readonly IUserQueryRepository _userQueryRepository;

    public GetUsersQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
    {
        _mapper = mapper;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var response = new List<UserResponse>();
        var users = await _userQueryRepository.FindAll();

        response.AddRange(_mapper.Map<List<UserResponse>>(users));
        return response;
    }
}
