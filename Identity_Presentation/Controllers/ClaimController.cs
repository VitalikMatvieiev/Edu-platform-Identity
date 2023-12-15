using Identity_Application.Commands.Claims;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Application.Queries.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimController : Controller
{
    private readonly IMediator _mediator;

    public ClaimController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetClaimById(int id)
    {
        var result = await _mediator
            .Send(new GetClaimByIdQuery(id));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClaims()
    {
        var result = await _mediator
            .Send(new GetAllClaimsQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClaim([FromBody] ClaimVM claim)
    {
        var result = await _mediator
            .Send(new CreateClaimCommand(claim));

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClaim(int id, [FromBody] ClaimVM claim)
    {
        await _mediator
            .Send(new UpdateClaimCommand(id, claim));

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteClaim(int id)
    {
        await _mediator
            .Send(new DeleteClaimCommand(id));

        return Ok();
    }
}