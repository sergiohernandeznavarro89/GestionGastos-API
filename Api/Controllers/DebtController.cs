using Application.Commands;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebtController : ControllerBase
{
    private readonly IMediator _mediator;

    public DebtController(IMediator mediator)
    {
        _mediator = mediator;
    }


    //[HttpGet]
    //[Route("[action]")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PendingPayItemResponse>))]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetPendingPayItems([FromQuery] int userId)
    //{
    //    try
    //    {
    //        var response = await _mediator.Send(new GetPendingPayItemQuery(userId));
    //        return Ok(response);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}       

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DebtResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDebts([FromQuery] int userId)
    {
        try
        {
            var response = await _mediator.Send(new GetDebtQuery(userId));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddDebtResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddDebt([FromBody] AddDebtCommand command)
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

    //[HttpPut]
    //[Route("[action]")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateItemResponse))]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command)
    //{
    //    try
    //    {
    //        var result = await _mediator.Send(command);
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}
}
