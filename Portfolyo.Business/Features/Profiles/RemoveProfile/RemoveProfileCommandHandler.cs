using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Profiles.RemoveProfile
{
    internal sealed class RemoveProfileCommandHandler : IRequestHandler<RemoveProfileCommand, ErrorOr<Unit>>
    {

        private readonly IProfileRepository profileRepository;
        private readonly IUnitOfWork unitOfWork;

        public RemoveProfileCommandHandler(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(RemoveProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = await profileRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);
            
            if(profile is null)
            {
                return Error.Conflict("NameIsExist","Profil Bulunamadı.");

            }
             profileRepository.Remove(profile);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
