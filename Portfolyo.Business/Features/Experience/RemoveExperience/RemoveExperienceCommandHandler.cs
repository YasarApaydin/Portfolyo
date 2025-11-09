using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Experience.RemoveExperience
{

    internal sealed class RemoveExperienceCommandHandler : IRequestHandler<RemoveExperienceCommand, ErrorOr<Unit>>
    {

        private readonly IExperienceRepository experienceRepository;
        private readonly IUnitOfWork unitOfWork;
        public RemoveExperienceCommandHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork)
        {
            this.experienceRepository = experienceRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(RemoveExperienceCommand request, CancellationToken cancellationToken)
        {
            Entities.Models.Experience experience = await experienceRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);


            if (experience is null)
            {
                return Error.Conflict("NameIsExist", "Deneyim Bulunamadı.");

            }
            experienceRepository.Remove(experience);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;



        }
    }
}
