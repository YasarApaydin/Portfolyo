using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Skills.CreateSkill
{
    internal sealed class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, ErrorOr<Unit>>
    {

        private readonly ISkillRepository skillRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateSkillCommandHandler(IUnitOfWork unitOfWork, ISkillRepository skillRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.skillRepository = skillRepository;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            bool isSkill = await skillRepository.AnyAsync(P => P.Name== request.Name,cancellationToken);

            if (isSkill)
            {
                return Error.Conflict("NameIsExists","Bu Yetenek Daha Önce Eklenmiştir.");
            }
            Skill skill = mapper.Map<Skill>(request);


            await skillRepository.AddAsync(skill,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
