namespace Application.QueryHandlers;

public class GetAllItemQueryHandler : IRequestHandler<GetItemQuery, List<ItemResponse>>
{

    private readonly IMapper _mapper;
    private readonly IItemSummaryQueryRepository _itemSummaryQueryRepository;

    public GetAllItemQueryHandler(IMapper mapper, IItemSummaryQueryRepository itemSummaryQueryRepository)
    {
        _mapper = mapper;
        _itemSummaryQueryRepository = itemSummaryQueryRepository;        
    }

    public async Task<List<ItemResponse>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var response = new List<ItemResponse>();        

        var items = await _itemSummaryQueryRepository.FindByUserId(request.UserId);        

        response = _mapper.Map<List<ItemResponse>>(items);

        return response;
    }    
}
