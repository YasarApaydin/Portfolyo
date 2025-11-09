using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Skills.CreateSkill
{
    public sealed record CreateSkillCommand(
        string Name,
        int Level,
        string IconClass,
        string Category

        ) :IRequest<ErrorOr<Unit>>;
}
