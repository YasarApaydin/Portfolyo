using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Profiles.CreateProfile
{
    public sealed record CreateProfileCommand(
        string FullName,
        string Title,
        string Biography,
        string Email,
        string GithubUrl,
        string LinkedInUrl
        ) :IRequest<ErrorOr<Unit>>;
}
