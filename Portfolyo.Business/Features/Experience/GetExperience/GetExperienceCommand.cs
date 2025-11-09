using MediatR;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Experience.GetExperience
{
    public sealed record GetExperienceCommand(): IRequest<List<Entities.Models.Experience>>;
}
