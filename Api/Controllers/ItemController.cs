using Application.Commands;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }    

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddItemResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddItem([FromBody] AddItemCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PendingPayItemResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingPayItems([FromQuery] int userId)
    {
        try
        {
            var response = await _mediator.Send(new GetPendingPayItemQuery(userId));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NextMonthPendingPayItemResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetNextMonthPendingPayItems([FromQuery] int userId)
    {
        try
        {
            var response = await _mediator.Send(new GetNextMonthPendingPayItemQuery(userId));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ItemResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllItems([FromQuery] int userId)
    {
        try
        {
            var response = await _mediator.Send(new GetItemQuery(userId));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
