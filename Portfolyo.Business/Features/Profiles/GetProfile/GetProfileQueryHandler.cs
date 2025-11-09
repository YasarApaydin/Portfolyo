using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Profiles.GetProfile
{
    internal sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, List<Profile>>
    {
        private readonly IProfileRepository profileRepository;

        public GetProfileQueryHandler(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }



        public async Task<List<Profile>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return await profileRepository.GetAll().OrderBy(p => p.FullName).ToListAsync(cancellationToken);
        }
    }
}
