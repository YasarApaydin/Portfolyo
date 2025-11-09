using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Experience.CreateExperience
{
    internal sealed class CreateExperinceCommandHandler : IRequestHandler<CreateExperienceCommand, ErrorOr<Unit>>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IExperienceRepository experienceRepository;
        private readonly IMapper mapper;


        public CreateExperinceCommandHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.experienceRepository = experienceRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            bool isCheckExperience = await experienceRepository.AnyAsync(p => p.CompanyName == request.CompanyName,cancellationToken);

       

            Entities.Models.Experience experience = mapper.Map<Entities.Models.Experience>(request);
            await experienceRepository.AddAsync(experience, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
