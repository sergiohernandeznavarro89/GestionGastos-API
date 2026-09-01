using Application.Dto.Transfer;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries;

public class GetNextMonthPendingTransferQuery : IRequest<List<NextMonthPendingTransferResponse>>
{
    public int UserId { get; set; }
}
