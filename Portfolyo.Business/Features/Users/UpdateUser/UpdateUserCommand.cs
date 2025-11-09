using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Users.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        string PhoneNumber,
        string UserName,
        string CurrentPassword,
        string NewPassword


        ) : IRequest<ErrorOr<Unit>>;
}
