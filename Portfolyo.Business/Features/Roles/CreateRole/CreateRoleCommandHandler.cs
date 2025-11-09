using MediatR;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Roles.CreateRole
{
    internal sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Unit>
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
        }




        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var checkIsRole = await roleRepository.AnyAsync(p => p.Name== request.Name,cancellationToken);
            if (checkIsRole)
            {
                throw new ArgumentException("Bu rol daha önce oluşturulmuştur.");

            }

            AppRole appRole = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name


            };
          await  roleRepository.AddAsync(appRole,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
}
