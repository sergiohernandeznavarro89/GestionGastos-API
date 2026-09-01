using Application.Dto.Transfer;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries;

public class GetPendingTransferQuery : IRequest<List<PendingTransferResponse>>
{
    public int UserId { get; set; }
}
