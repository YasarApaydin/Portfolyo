using MediatR;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Projects.GetProject
{
    public sealed record GetProjectQuery():IRequest<List<Project>>;
}
