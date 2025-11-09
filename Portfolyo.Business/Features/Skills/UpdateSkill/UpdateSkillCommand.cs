using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Skills.UpdateSkill
{
    public sealed record UpdateSkillCommand(
        Guid Id,
         string Name,
        int Level,
        string IconClass,
        string Category
        ) :IRequest<ErrorOr<Unit>>;
}
