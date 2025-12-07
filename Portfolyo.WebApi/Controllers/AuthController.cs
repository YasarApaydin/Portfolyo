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
            var response = await mediator.Send(loginCommand, cancellationToken);
            if (response == null)
                return BadRequest(new { success = false, message = "Giriş Başarısız." });



            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            Response.Cookies.Append("accessToken", response.AccessToken, accessCookieOptions);

            return Ok(new
            {
                success = true,
                message = "Giriş başarılı.",
                data = response
            });
        }



        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Append("accessToken", "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { success = true, message = "Çıkış başarılı." });
        }


    }
}
