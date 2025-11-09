using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Profiles.RemoveProfile
{
    public sealed record RemoveProfileCommand(
        Guid Id
        ):IRequest<ErrorOr<Unit>>;
}
