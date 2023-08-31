namespace Application.QueryHandlers;

public class GetAccountsByUserQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountResponse>>
{

    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public GetAccountsByUserQueryHandler(IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<List<AccountResponse>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<AccountResponse>();
        response = _mapper.Map<List<AccountResponse>>(await _accountQueryRepository.FindByUserId(request.UserId));

        return response;
    }
}
