using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Skills.UpdateSkill
{
    internal sealed class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, ErrorOr<Unit>>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ISkillRepository skillRepository;
        private readonly IMapper mapper;


        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.skillRepository = skillRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            Skill skill = await skillRepository.GetByIdAsync(p => p.Id== request.Id,cancellationToken);
            if (skill is null)
            {
                return Error.Conflict("NameIsExists", "Yetenek Bulunamadı");
                
            }

            if (skill.Name != request.Name)
            {
                bool isCheckSkill = await skillRepository.AnyAsync(P => P.Name == request.Name, cancellationToken);
                if (isCheckSkill)
                {
                    return Error.Conflict("NameIsExist", "Bu Başlıklı Yetenek Vardır.");
                }
            }
            mapper.Map(request, skill);



            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;




        }
    }
}
