using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Portfolyo.Business.Features.Auth.Login;
using Portfolyo.WebApi.Abstractions;

namespace Portfolyo.WebApi.Controllers
{
  
    public sealed class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [EnableRateLimiting("LoginPolicy")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            try
            {
                var response = await mediator.Send(loginCommand, cancellationToken);

                if (response == null)
                {
                return BadRequest(new { success = false, message = "Giriş Başarısız." });
                }



                return Ok(new
                {
                    success = true,
                    message = "Giriş başarılı.",
                    data = response
                });
            }
            catch (Exception ex)
            {
               
                return Ok(new
                {
                    success = false,
                    message = "Giriş Başarısız."
                });
            }

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/admin")]
        public IActionResult GetAdminPage()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin.html"), "text/html");
        }



    }
}
