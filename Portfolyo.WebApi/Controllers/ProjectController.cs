using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Business.Features.Projects.CreateProject;
using Portfolyo.Business.Features.Projects.GetProject;
using Portfolyo.Business.Features.Projects.RemoveProject;
using Portfolyo.Business.Features.Projects.UpdateProject;
using Portfolyo.DataAccess.Authorization;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
    public class ProjectController : ApiController
    {
        public ProjectController(IMediator mediator) : base(mediator)
        {
        }




        [HttpPost]
        [RoleFilter("Project.Add")]
        public async Task<IActionResult> Add(CreateProjectCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Project.Update")]
        public async Task<IActionResult> Update(UpdateProjectCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Project.Remove")]
        public async Task<IActionResult> RemoveById(RemoveProjectCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetProjectQuery request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);


            return Ok(response);
        }








    }
}
