using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Profiles.UpdateProfile
{
    public sealed record UpdateProfileCommand(
        Guid Id,
         string FullName,
        string Title,
        string Biography,
        string Email,
        string GithubUrl,
        string LinkedInUrl
        ) :IRequest<ErrorOr<Unit>>;
}
