using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Projects.UpdateProject
{
    public sealed record UpdateProjectCommand(
           Guid Id,
           string Title,
         string Description,
         string ImageUrl,
         string GithubUrl,
          string Technologies
        ) :IRequest<ErrorOr<Unit>>;
}
