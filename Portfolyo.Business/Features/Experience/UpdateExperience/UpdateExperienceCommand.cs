using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Experience.UpdateExperience
{
    public sealed record UpdateExperienceCommand(
        Guid Id,
          string CompanyName,
        string Position,
        string Sector,
        DateTime StartDate,
        DateTime EndDate,
        string WorkingMethod,
        string Explanation,
        string TechnologyUsed

        ) : IRequest<ErrorOr<Unit>>;
}
