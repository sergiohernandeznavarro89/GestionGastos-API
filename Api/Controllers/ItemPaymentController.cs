using Application.Commands;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemPaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemPaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }    

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddItemPaymentResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddItemPayment([FromQuery] int itemId)
    {
        try
        {
            var result = await _mediator.Send(new AddItemPaymentCommand(itemId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }    
}
