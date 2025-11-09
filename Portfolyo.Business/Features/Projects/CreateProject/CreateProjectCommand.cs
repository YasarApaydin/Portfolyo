using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Projects.CreateProject
{
    public sealed record CreateProjectCommand(
         string Title,
         string Description,
         string ImageUrl,
         string GithubUrl,
          string Technologies
        ) :IRequest<ErrorOr<Unit>>;
}
