using System.Collections.Generic;
using Application.Dto;
using MediatR;

namespace Application.Queries;

public class GetExecutedItemQuery : IRequest<List<ItemResponse>>
{
    public int UserId { get; set; }
    public int MonthsOffset { get; set; }

    public GetExecutedItemQuery(int userId, int monthsOffset = 0)
    {
        UserId = userId;
        MonthsOffset = monthsOffset;
    }
}
