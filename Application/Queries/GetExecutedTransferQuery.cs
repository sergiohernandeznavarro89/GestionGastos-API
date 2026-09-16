using System.Collections.Generic;
using Application.Dto.Transfer;
using MediatR;

namespace Application.Queries;

public class GetExecutedTransferQuery : IRequest<List<TransferResponse>>
{
    public int UserId { get; set; }
    public int MonthsOffset { get; set; }

    public GetExecutedTransferQuery(int userId, int monthsOffset = 0)
    {
        UserId = userId;
        MonthsOffset = monthsOffset;
    }
}
