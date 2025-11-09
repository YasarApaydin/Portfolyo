using MediatR;

namespace Portfolyo.Business.Features.Roles.CreateRole
{
    public sealed record CreateRoleCommand(
        string Name
        ):IRequest<Unit>;
}
