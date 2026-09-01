using Application.Dto.Transfer;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries;

public class GetTransferQuery : IRequest<List<TransferResponse>>
{
    public int UserId { get; set; }
}
