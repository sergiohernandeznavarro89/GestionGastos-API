using Application.Dto.Transfer;
using Application.Queries;
using AutoMapper;
using Domain.Enums;
using Domain.Repositories.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.QueryHandlers;

public class GetPendingTransferQueryHandler : IRequestHandler<GetPendingTransferQuery, List<PendingTransferResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITransferSummaryQueryRepository _transferSummaryQueryRepository;
    private readonly ITransferPaymentQueryRepository _transferPaymentQueryRepository;

    public GetPendingTransferQueryHandler(IMapper mapper, ITransferSummaryQueryRepository transferSummaryQueryRepository, ITransferPaymentQueryRepository transferPaymentQueryRepository)
    {
        _mapper = mapper;
        _transferSummaryQueryRepository = transferSummaryQueryRepository;
        _transferPaymentQueryRepository = transferPaymentQueryRepository;
    }

    public async Task<List<PendingTransferResponse>> Handle(GetPendingTransferQuery request, CancellationToken cancellationToken)
    {
        var response = new List<PendingTransferResponse>();        

        var transfers = await _transferSummaryQueryRepository.FindByUserId(request.UserId);

        var transfersRecurrentes = transfers.Where(x => x.PeriodTypeId == (int)PeriodTypeEnum.Recurrente).ToList();

        var transfersPendingPay = transfersRecurrentes
            .Where(t => ShouldPayThisMonth(t.StartDate, t.EndDate, (int)t.Periodity))
            .Where(t => _transferPaymentQueryRepository.FindByTransferAndThisMonth(t.TransferId).Result is null)
            .ToList();

        response = _mapper.Map<List<PendingTransferResponse>>(transfersPendingPay);

        return response;
    }

    private bool ShouldPayThisMonth(DateTime startDate, DateTime endDate, int period)
    {
        DateTime currentDate = DateTime.Now.Date;

        if (currentDate >= startDate && currentDate <= endDate)
        {
            int monthsDifference = (currentDate.Year - startDate.Year) * 12 + currentDate.Month - startDate.Month;

            if (monthsDifference % period == 0)
            {
                return true;
            }
        }

        return false;
    }
}
