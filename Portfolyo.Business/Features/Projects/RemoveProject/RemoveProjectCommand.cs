using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Projects.RemoveProject
{
    public sealed record RemoveProjectCommand(
        Guid Id
        ):IRequest<ErrorOr<Unit>>;
}
