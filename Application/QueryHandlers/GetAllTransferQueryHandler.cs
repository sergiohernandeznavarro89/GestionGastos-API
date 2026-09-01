using Application.Dto.Transfer;
using Application.Queries;
using AutoMapper;
using Domain.Repositories.Query;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.QueryHandlers;

public class GetAllTransferQueryHandler : IRequestHandler<GetTransferQuery, List<TransferResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITransferSummaryQueryRepository _transferSummaryQueryRepository;

    public GetAllTransferQueryHandler(IMapper mapper, ITransferSummaryQueryRepository transferSummaryQueryRepository)
    {
        _mapper = mapper;
        _transferSummaryQueryRepository = transferSummaryQueryRepository;
    }

    public async Task<List<TransferResponse>> Handle(GetTransferQuery request, CancellationToken cancellationToken)
    {
        var response = new List<TransferResponse>();        
        var transfers = await _transferSummaryQueryRepository.FindByUserId(request.UserId);
        response = _mapper.Map<List<TransferResponse>>(transfers);
        return response;
    }
}
