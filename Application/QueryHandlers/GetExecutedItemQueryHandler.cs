using Application.Dto;
using Application.Queries;
using AutoMapper;
using Domain.Repositories.Query;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.QueryHandlers;

public class GetExecutedItemQueryHandler : IRequestHandler<GetExecutedItemQuery, List<ItemResponse>>
{
    private readonly IMapper _mapper;
    private readonly IItemSummaryQueryRepository _itemSummaryQueryRepository;

    public GetExecutedItemQueryHandler(IMapper mapper, IItemSummaryQueryRepository itemSummaryQueryRepository)
    {
        _mapper = mapper;
        _itemSummaryQueryRepository = itemSummaryQueryRepository;
    }

    public async Task<List<ItemResponse>> Handle(GetExecutedItemQuery request, CancellationToken cancellationToken)
    {
        var items = await _itemSummaryQueryRepository.FindExecutedByMonth(request.UserId, request.MonthsOffset);
        return _mapper.Map<List<ItemResponse>>(items);
    }
}
