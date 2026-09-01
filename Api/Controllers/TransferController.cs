using Application.Commands.Transfer;
using Application.Dto.Transfer;
using Application.Dto.TransferPayment;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransferController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("pendingPay")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PendingTransferResponse>))]
    public async Task<IActionResult> GetPendingPayTransfers([FromQuery] int userId)
    {
        var response = await _mediator.Send(new GetPendingTransferQuery { UserId = userId });
        return Ok(response);
    }

    [HttpGet("nextMonthPendingPay")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NextMonthPendingTransferResponse>))]
    public async Task<IActionResult> GetNextMonthPendingPayTransfers([FromQuery] int userId)
    {
        var response = await _mediator.Send(new GetNextMonthPendingTransferQuery { UserId = userId });
        return Ok(response);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransferResponse>))]
    public async Task<IActionResult> GetAllTransfers([FromQuery] int userId)
    {
        var response = await _mediator.Send(new GetTransferQuery { UserId = userId });
        return Ok(response);
    }

    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddTransferResponse))]
    public async Task<IActionResult> Add([FromBody] AddTransferCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTransferResponse))]
    public async Task<IActionResult> Update([FromBody] UpdateTransferCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteTransferResponse))]
    public async Task<IActionResult> Delete([FromQuery] int transferId)
    {
        var response = await _mediator.Send(new DeleteTransferCommand { TransferId = transferId });
        return Ok(response);
    }
    
    [HttpPost("addPayment")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddTransferPaymentResponse))]
    public async Task<IActionResult> AddPayment([FromBody] AddTransferPaymentCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
