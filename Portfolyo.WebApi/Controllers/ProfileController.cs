using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Business.Features.Profiles.CreateProfile;
using Portfolyo.Business.Features.Profiles.GetProfile;
using Portfolyo.Business.Features.Profiles.RemoveProfile;
using Portfolyo.Business.Features.Profiles.UpdateProfile;
using Portfolyo.DataAccess.Authorization;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
    public sealed class ProfileController : ApiController
    {
        public ProfileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [RoleFilter("Profile.Add")]
        public async Task<IActionResult> Add(CreateProfileCommand request,CancellationToken cancellation)
        {
          var response =  await mediator.Send(request,cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Profile.Update")]
        public async Task<IActionResult> Update(UpdateProfileCommand request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

            if (response.IsError)
            {
                return BadRequest(response.FirstError);
            }
            return NoContent();
        }


        [HttpPost]
        [RoleFilter("Profile.Remove")]
        public async Task<IActionResult> RemoveById(RemoveProfileCommand request, CancellationToken cancellation)
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
        public async Task<IActionResult> GetAll([FromQuery]  GetProfileQuery request, CancellationToken cancellation)
        {
            var response = await mediator.Send(request, cancellation);

           
            return Ok(response);
        }



    }
}
