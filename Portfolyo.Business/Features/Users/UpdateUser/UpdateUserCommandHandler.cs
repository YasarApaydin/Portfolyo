using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Users.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>>
    {
        private readonly UserManager<AppUser> userManager;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if(user is null)
            {
                return Error.NotFound("User.NotFound", "Kullanıcı bulunamadı.");
            }


            user.Name = request.Name;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;

            user.NormalizedEmail = request.Email.ToUpperInvariant();
            user.NormalizedUserName = request.Email.ToUpperInvariant();

            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(request.CurrentPassword))
                {
                    return Error.Validation("User.MissingCurrentPassword", "Şifre değiştirmek için mevcut şifrenizi girmeniz gerekiyor.");
                }

                var changeResult = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (!changeResult.Succeeded)
                {

                    var passwordErrors = changeResult.Errors
                      .Select(e => Error.Failure("User.ChangePasswordFailed", e.Description))
                      .ToList();
                    return passwordErrors;
                }
            }
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var updateErrors = updateResult.Errors.Select(e => Error.Failure("User.UpdateFailed", e.Description)).ToList();
                return updateErrors;
            }



            return Unit.Value;

        }
    }
}
