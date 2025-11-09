using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Skills.RemoveSkill
{
    public sealed record RemoveSkillCommand(
        Guid Id
        
        ):IRequest<ErrorOr<Unit>>;
}
