using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Profiles.CreateProfile
{
    internal sealed class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, ErrorOr<Unit>>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateProfileCommandHandler(IUnitOfWork unitOfWork, IProfileRepository profileRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            Entities.Models.Profile profile = mapper.Map<Entities.Models.Profile>(request);


            await profileRepository.AddAsync(profile,cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        
        
        }
    }
}
