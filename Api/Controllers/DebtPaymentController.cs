using Application.Commands;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebtPaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DebtPaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }    

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddDebtPaymentResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddDebtPayment(AddDebtPaymentCommand paymentCommand)
    {
        try
        {
            var result = await _mediator.Send(paymentCommand);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }    
}
