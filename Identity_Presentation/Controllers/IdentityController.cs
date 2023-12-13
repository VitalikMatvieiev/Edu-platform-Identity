using Identity_Application.Commands;
using Identity_Application.Queries;
using Identity_Domain.Entities.Base;
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

    [HttpGet]
    public async Task<IActionResult> GetAllIdentities()
    {
        var result = await _mediator.Send(new GetAllIdentitiesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIdentity(string Username, string Email, string Password)
    {
        var command = new CreateIdentityCommand(Username, Email, Password);
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditIdentity(Identity identity)
    {
        var command = new UpdateIdentityCommand(identity);
        //var result = await _mediator.Send(command);

        return Ok();
    }
}