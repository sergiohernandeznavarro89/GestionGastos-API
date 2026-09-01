using Application.Dto.Transfer;
using MediatR;

namespace Application.Commands.Transfer;

public class DeleteTransferCommand : IRequest<DeleteTransferResponse>
{
    public int TransferId { get; set; }
}
