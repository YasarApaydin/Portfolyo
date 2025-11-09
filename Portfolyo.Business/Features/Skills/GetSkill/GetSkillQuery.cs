using MediatR;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Skills.GetSkill
{
    public sealed record GetSkillQuery():IRequest<List<Skill>>;
}
