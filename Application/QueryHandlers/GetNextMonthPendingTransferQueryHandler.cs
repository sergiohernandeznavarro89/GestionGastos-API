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

public class GetNextMonthPendingTransferQueryHandler : IRequestHandler<GetNextMonthPendingTransferQuery, List<NextMonthPendingTransferResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITransferSummaryQueryRepository _transferSummaryQueryRepository;
    private readonly ITransferPaymentQueryRepository _transferPaymentQueryRepository;

    public GetNextMonthPendingTransferQueryHandler(IMapper mapper, ITransferSummaryQueryRepository transferSummaryQueryRepository, ITransferPaymentQueryRepository transferPaymentQueryRepository)
    {
        _mapper = mapper;
        _transferSummaryQueryRepository = transferSummaryQueryRepository;
        _transferPaymentQueryRepository = transferPaymentQueryRepository;
    }

    public async Task<List<NextMonthPendingTransferResponse>> Handle(GetNextMonthPendingTransferQuery request, CancellationToken cancellationToken)
    {
        var response = new List<NextMonthPendingTransferResponse>();        

        var transfers = await _transferSummaryQueryRepository.FindByUserId(request.UserId);

        var transfersRecurrentes = transfers.Where(x => x.PeriodTypeId == (int)PeriodTypeEnum.Recurrente).ToList();

        var transfersPendingPay = transfersRecurrentes
            .Where(t => ShouldPayNextMonth(t.StartDate, t.EndDate, (int)t.Periodity))
            .Where(t => _transferPaymentQueryRepository.FindByTransferAndNextMonth(t.TransferId).Result is null)
            .ToList();

        response = _mapper.Map<List<NextMonthPendingTransferResponse>>(transfersPendingPay);

        return response;
    }

    private bool ShouldPayNextMonth(DateTime startDate, DateTime endDate, int period)
    {
        DateTime nextMonthDate = DateTime.Now.Date.AddMonths(1);

        if (nextMonthDate >= startDate && nextMonthDate <= endDate)
        {
            int monthsDifference = (nextMonthDate.Year - startDate.Year) * 12 + nextMonthDate.Month - startDate.Month;

            if (monthsDifference % period == 0)
            {
                return true;
            }
        }

        return false;
    }
}
