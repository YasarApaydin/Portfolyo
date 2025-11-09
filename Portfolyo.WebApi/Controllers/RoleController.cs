using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Business.Features.Roles.CreateRole;
using Portfolyo.Business.Features.Roles.GetRole;
using Portfolyo.DataAccess.Authorization;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
    public class RoleController : ApiController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [RoleFilter("Role.Add")]
        public async Task<IActionResult> Add(CreateRoleCommand createRole,CancellationToken  cancellationToken)
        {
            await mediator.Send(createRole, cancellationToken);
            return NoContent();
        }
        [HttpGet]
        [RoleFilter("Role.GetAll")]
        public async Task<IActionResult> GetAll(GetRoleQuery getRole, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(getRole, cancellationToken);
            return Ok(response);
        }

    }
}
