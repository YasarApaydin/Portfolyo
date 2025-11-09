using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Experience.GetExperience
{
    internal sealed class GetExperienceCommandHandler : IRequestHandler<GetExperienceCommand, List<Entities.Models.Experience>>
    {

        private readonly IExperienceRepository experienceRepository;

        public GetExperienceCommandHandler(IExperienceRepository experienceRepository)
        {
            this.experienceRepository = experienceRepository;
        }

        public async Task<List<Entities.Models.Experience>> Handle(GetExperienceCommand request, CancellationToken cancellationToken)
        {
            return await experienceRepository.GetAll().OrderBy(p => p.CompanyName).ToListAsync(cancellationToken);
        }
    }
}
