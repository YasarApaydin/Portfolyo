using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Projects.GetProject
{
    internal sealed class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, List<Project>>
    {
        private readonly IProjectRepository projectRepository;

        public GetProjectQueryHandler(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<List<Project>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            return await projectRepository.GetAll().OrderBy(p => p.Title).ToListAsync(cancellationToken);
        }
    }
}
