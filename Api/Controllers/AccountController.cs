using Application.Commands;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AccountResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccountsByUser([FromQuery] int userId)
    {
        try
        {
            var response = await _mediator.Send(new GetAccountsQuery(userId));
            return Ok(response);
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddAccountResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAccount([FromBody] AddAccountCommand command)
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

    [HttpDelete]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteAccountResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAccount([FromQuery] int accountId)
    {
        try
        {
            var result = await _mediator.Send(new DeleteAccountCommand(accountId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            var result = new DeleteAccountResponse { Success = false, Message = ex.Message };
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
