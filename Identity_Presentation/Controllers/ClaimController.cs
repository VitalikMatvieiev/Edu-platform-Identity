using Identity_Application.Commands.Claims;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Application.Queries.Claims;
using Identity_Domain.Entities.Enums;
using Identity_Infrastructure.Authentication;
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

    [HasClaim(ControllersClaims.ReadClaims)]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetClaimById(int id)
    {
        var result = await _mediator
            .Send(new GetClaimByIdQuery(id));

        return Ok(result);
    }

    [HasClaim(ControllersClaims.ReadClaims)]
    [HttpGet]
    public async Task<IActionResult> GetAllClaims()
    {
        var result = await _mediator
            .Send(new GetAllClaimsQuery());

        return Ok(result);
    }

    [HasClaim(ControllersClaims.WriteClaim)]
    [HttpPost]
    public async Task<IActionResult> CreateClaim([FromBody] ClaimVM claim)
    {
        var result = await _mediator
            .Send(new CreateClaimCommand(claim));

        return Ok(result);
    }

    [HasClaim(ControllersClaims.ChangeClaim)]
    [HttpPut]
    public async Task<IActionResult> UpdateClaim(int id, [FromBody] ClaimVM claim)
    {
        await _mediator
            .Send(new UpdateClaimCommand(id, claim));

        return Ok();
    }

    [HasClaim(ControllersClaims.DeleteClaim)]
    [HttpDelete]
    public async Task<IActionResult> DeleteClaim(int id)
    {
        await _mediator
            .Send(new DeleteClaimCommand(id));

        return Ok();
    }
}