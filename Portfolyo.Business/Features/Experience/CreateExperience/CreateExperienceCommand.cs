using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Experience.CreateExperience
{
    public sealed record CreateExperienceCommand(
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
