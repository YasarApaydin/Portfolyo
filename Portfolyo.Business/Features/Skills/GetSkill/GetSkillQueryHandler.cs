using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Skills.GetSkill
{
    internal sealed class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, List<Skill>>
    {
        private readonly ISkillRepository skillRepository;

        public GetSkillQueryHandler(ISkillRepository skillRepository)
        {
            this.skillRepository = skillRepository;
        }

        public async Task<List<Skill>> Handle(GetSkillQuery request, CancellationToken cancellationToken)
        {
            return await skillRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);
        }
    }
}
