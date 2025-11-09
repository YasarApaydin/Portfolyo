using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Business.Features.Skills.UpdateSkill;
using Portfolyo.Business.Features.Users.GetUser;
using Portfolyo.Business.Features.Users.UpdateUser;
using Portfolyo.DataAccess.Authorization;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }





        [HttpGet]
        [RoleFilter("User.GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetUserQuery request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);


            return Ok(response);
        }
        


        [HttpPost]
        [RoleFilter("User.Update")]
        public async Task<IActionResult> Update(UpdateUserCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }



    }
}
