using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Experience.UpdateExperience
{
    internal sealed class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, ErrorOr<Unit>>
    {
        private readonly IExperienceRepository experienceRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateExperienceCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IExperienceRepository experienceRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.experienceRepository = experienceRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            Entities.Models.Experience experience = await experienceRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);

            if(experience is null)
            {
                return Error.Conflict("NameIsExists", "Deneyim Bulunamadı.");
            }

            if (experience.CompanyName != request.CompanyName)
            {
                bool isCheckProje = await experienceRepository.AnyAsync(P => P.CompanyName == request.CompanyName, cancellationToken);
                if (isCheckProje)
                {
                    return Error.Conflict("NameIsExist", "Bu Başlıklı Deneyim Vardır.");
                }
            }


            mapper.Map(request, experience);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;


        }
    }
}
