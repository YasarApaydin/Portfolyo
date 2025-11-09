using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Skills.RemoveSkill
{
    internal sealed class RemoveSkillCommandHandler : IRequestHandler<RemoveSkillCommand, ErrorOr<Unit>>
    {

        private readonly ISkillRepository skillRepository;
        private readonly IUnitOfWork unitOfWork;


        public RemoveSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            this.skillRepository = skillRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(RemoveSkillCommand request, CancellationToken cancellationToken)
        {
            Skill skill = await skillRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);

            if (skill is null)
            {
                return Error.Conflict("NameIsExists","Skill Bulunamadı.");

            }
            skillRepository.Remove(skill);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
