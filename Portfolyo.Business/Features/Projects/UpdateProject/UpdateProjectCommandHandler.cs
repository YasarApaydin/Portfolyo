using AutoMapper;
using ErrorOr;
using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Projects.UpdateProject
{
    internal sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ErrorOr<Unit>>
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await projectRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);
            if(project is null)
            {
                return Error.Conflict("NameIsExist", "Proje Bulunmadı");
            }

            if(project.Title != request.Title)
            {
                bool isCheckProje = await projectRepository.AnyAsync(P => P.Title == request.Title, cancellationToken);
                if (isCheckProje)
                {
                    return Error.Conflict("NameIsExist", "Bu Başlıklı Proje Vardır.");
                }}

            mapper.Map(request,project);


            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
