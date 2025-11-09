using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Profiles.UpdateProfile
{
    internal sealed class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProfileRepository profileRepository;
        private readonly IMapper mapper;


        public UpdateProfileCommandHandler(IProfileRepository profileRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            Entities.Models.Profile profile = await profileRepository.GetByIdAsync(x => x.Id == request.Id,cancellationToken);
            if(profile is null)
            {
                return Error.Conflict("NameIsExist","Profil Bulunmadı");
            }
            if(profile.FullName != request.FullName)
            {
                bool isCheckProfile = await profileRepository.AnyAsync(x => x.FullName == request.FullName,cancellationToken);

                if (isCheckProfile)
                {
                    return Error.Conflict("NameIsExist","Bu Profil İsminde Profil Vardır.");
                }}
            mapper.Map(request,profile);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
