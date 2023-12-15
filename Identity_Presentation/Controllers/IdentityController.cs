using Identity_Application.Commands.Identities;
using Identity_Application.Models.BaseEntitiesModels;
using Identity_Application.Queries.Identities;
using Identity_Domain.Entities.Enums;
using Identity_Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : Controller
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HasClaim(ControllersClaims.ReadOwnIdentity)]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetIdentityById(int id)
    {
        var result = await _mediator
            .Send(new GetIdentityByIdQuery(id));

        return Ok(result);
    }

    [HasClaim(ControllersClaims.ReadIdentities)]
    [HttpGet]
    public async Task<IActionResult> GetAllIdentities()
    {
        var result = await _mediator
            .Send(new GetAllIdentitiesQuery());

        return Ok(result);
    }

    [HasClaim(ControllersClaims.WriteIdentity)]
    [HttpPost]
    public async Task<IActionResult> CreateIdentity([FromBody] IdentityVM identityVM)
    {
        var result = await _mediator
            .Send(new CreateIdentityCommand(identityVM));

        return Ok(result);
    }

    [HasClaim(ControllersClaims.ChangeIdentity)]
    [HttpPut]
    public async Task<IActionResult> UpdateIdentity(int id, [FromBody] IdentityVM identityVM)
    {
        await _mediator
            .Send(new UpdateIdentityCommand(id, identityVM));

        return Ok();
    }

    [HasClaim(ControllersClaims.DeleteIdentity)]
    [HttpDelete]
    public async Task<IActionResult> DeleteIdentity(int id)
    {
        await _mediator
            .Send(new DeleteIdentityCommand(id));

        return Ok();
    }
}