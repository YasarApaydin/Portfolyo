using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Projects.RemoveProject
{
    internal sealed class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, ErrorOr<Unit>>
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUnitOfWork unitOfWork;
        public RemoveProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            this.projectRepository = projectRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await projectRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);

            if (project is null)
            {
                return Error.Conflict("NameIsExist","Proje Bulunamadı");
            }

            projectRepository.Remove(project);
            await unitOfWork.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}
