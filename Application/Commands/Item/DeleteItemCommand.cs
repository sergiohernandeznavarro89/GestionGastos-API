using MediatR;
using Application.Dto;

namespace Application.Commands;

public class DeleteItemCommand : IRequest<DeleteItemResponse>
{
    public int ItemId { get; set; }
}
