using Identity_Application.Commands.Roles;
using Identity_Application.Queries.Roles;
using Identity_Domain.Entities.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : Controller
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var result = await _mediator
            .Send(new GetRoleByIdQuery(id));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _mediator
            .Send(new GetAllRolesQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(Role Role)
    {
        var result = await _mediator
            .Send(new CreateRoleCommand(Role));

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(Role Role)
    {
        await _mediator
            .Send(new UpdateRoleCommand(Role));

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(int id)
    {
        await _mediator
            .Send(new DeleteRoleCommand(id));

        return Ok();
    }

}