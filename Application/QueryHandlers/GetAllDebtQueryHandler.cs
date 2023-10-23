namespace Application.QueryHandlers;

public class GetAllDebtQueryHandler : IRequestHandler<GetDebtQuery, List<DebtResponse>>
{

    private readonly IMapper _mapper;
    private readonly IDebtSummaryQueryRepository _debtSummaryQueryRepository;

    public GetAllDebtQueryHandler(IMapper mapper, IDebtSummaryQueryRepository debtSummaryQueryRepository)
    {
        _mapper = mapper;
        _debtSummaryQueryRepository = debtSummaryQueryRepository;        
    }

    public async Task<List<DebtResponse>> Handle(GetDebtQuery request, CancellationToken cancellationToken)
    {
        var response = new List<DebtResponse>();        

        var items = await _debtSummaryQueryRepository.FindByUserId(request.UserId);        

        response = _mapper.Map<List<DebtResponse>>(items);

        return response;
    }    
}
