using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Business.Features.Experience.CreateExperience;
using Portfolyo.Business.Features.Experience.GetExperience;
using Portfolyo.Business.Features.Experience.RemoveExperience;
using Portfolyo.Business.Features.Experience.UpdateExperience;
using Portfolyo.DataAccess.Authorization;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
    public class ExperienceController : ApiController
    {
        public ExperienceController(IMediator mediator) : base(mediator)
        {
        }




        [HttpPost]
        [RoleFilter("Experience.Add")]
        public async Task<IActionResult> Add(CreateExperienceCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Experience.Update")]
        public async Task<IActionResult> Update(UpdateExperienceCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Experience.Remove")]
        public async Task<IActionResult> RemoveById(RemoveExperienceCommand request, CancellationToken cancellation)
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
        public async Task<IActionResult> GetAll([FromQuery] GetExperienceCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);


            return Ok(response);
        }




    }
}
