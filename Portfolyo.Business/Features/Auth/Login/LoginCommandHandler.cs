using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Common;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Auth.Login
{
    internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IJwtProvider jwtProvider;
        public LoginCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider)
        {
            this.userManager = userManager;
            this.jwtProvider = jwtProvider;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);
        if(user is null)
            {
               
                throw new ArgumentException("Kullanıcı Bulunamadı");
            }


            bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
            {
             
                throw new ArgumentException("Şifre Yanlış");
            }

            string token = await jwtProvider.CreateTokenAsync(user);



            return new LoginCommandResponse(AccessToken: token, UserId: user.Id);
        }
    }
}
