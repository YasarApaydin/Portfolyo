using MediatR;

namespace Portfolyo.Business.Features.Roles.GetRole
{
    public sealed record GetRoleQuery():IRequest<List<GetRoleQueryResponse>>;
}
