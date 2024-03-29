﻿using Application.Commands;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubCategoryResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSubCategoriesByUser([FromQuery] int userId)
    {
        try
        {
            var response = await _mediator.Send(new GetSubCategoriesQuery(userId));
            return Ok(response);
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddSubCategoryResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddSubCategory([FromBody] AddSubCategoryCommand command)
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

    [HttpPut]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateSubCategoryResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryCommand command)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteSubCategoryResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSubCategory([FromQuery] int subCategoryId)
    {
        try
        {
            var result = await _mediator.Send(new DeleteSubCategoryCommand(subCategoryId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            var result = new DeleteAccountResponse { Success = false, Message = ex.Message };
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
