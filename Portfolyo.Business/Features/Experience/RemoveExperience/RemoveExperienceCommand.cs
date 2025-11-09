using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Experience.RemoveExperience
{
    public sealed record RemoveExperienceCommand(
        Guid Id
        ): IRequest<ErrorOr<Unit>>;
}
