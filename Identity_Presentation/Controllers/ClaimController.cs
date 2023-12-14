using Identity_Application.Commands.Claims;
using Identity_Application.Queries.Claims;
using Identity_Domain.Entities.Base;
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
    public async Task<IActionResult> CreateClaim(string Name)
    {
        var result = await _mediator
            .Send(new CreateClaimCommand(Name));

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClaim(Claim Claim)
    {
        await _mediator
            .Send(new UpdateClaimCommand(Claim));

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