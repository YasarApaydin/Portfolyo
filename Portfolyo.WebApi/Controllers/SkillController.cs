using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Business.Features.Skills.CreateSkill;
using Portfolyo.Business.Features.Skills.GetSkill;
using Portfolyo.Business.Features.Skills.RemoveSkill;
using Portfolyo.Business.Features.Skills.UpdateSkill;
using Portfolyo.DataAccess.Authorization;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
    public class SkillController : ApiController
    {
        public SkillController(IMediator mediator) : base(mediator)
        {
        }





        [HttpPost]
        [RoleFilter("Skill.Add")]
        public async Task<IActionResult> Add(CreateSkillCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Skill.Update")]
        public async Task<IActionResult> Update(UpdateSkillCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Skill.Remove")]
        public async Task<IActionResult> RemoveById(RemoveSkillCommand request, CancellationToken cancellation)
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
        public async Task<IActionResult> GetAll([FromQuery]  GetSkillQuery request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);


            return Ok(response);
        }











    }
}
