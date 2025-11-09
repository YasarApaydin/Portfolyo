using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Projects.CreateProject
{
    internal sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ErrorOr<Unit>>
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            bool isProject = await projectRepository.AnyAsync(p => p.Title == request.Title, cancellationToken);
            if (isProject)
            {
                return Error.Conflict("NameIsExist","Bu Başlıklı İsimde Proje Vardır.");
            }


            Project project = mapper.Map<Project>(request);

            await projectRepository.AddAsync(project, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;




        }
    }
}
