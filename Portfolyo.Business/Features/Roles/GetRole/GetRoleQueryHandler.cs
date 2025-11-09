using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.Business.Features.Roles.GetRole
{
    internal sealed class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<GetRoleQueryResponse>>
    {

        private readonly IRoleRepository roleRepository;

        public GetRoleQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<List<GetRoleQueryResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
           var response = await roleRepository.GetAll().Select(p => new GetRoleQueryResponse(p.Id,p.Name)).ToListAsync(cancellationToken);
            return response;
        }
    }
}
