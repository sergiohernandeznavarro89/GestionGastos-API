using Application.Dto.Transfer;
using Application.Queries;
using AutoMapper;
using Domain.Repositories.Query;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.QueryHandlers;

public class GetExecutedTransferQueryHandler : IRequestHandler<GetExecutedTransferQuery, List<TransferResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITransferSummaryQueryRepository _transferSummaryQueryRepository;

    public GetExecutedTransferQueryHandler(IMapper mapper, ITransferSummaryQueryRepository transferSummaryQueryRepository)
    {
        _mapper = mapper;
        _transferSummaryQueryRepository = transferSummaryQueryRepository;
    }

    public async Task<List<TransferResponse>> Handle(GetExecutedTransferQuery request, CancellationToken cancellationToken)
    {
        var transfers = await _transferSummaryQueryRepository.FindExecutedByMonth(request.UserId, request.MonthsOffset);
        return _mapper.Map<List<TransferResponse>>(transfers);
    }
}
