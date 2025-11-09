using MediatR;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Profiles.GetProfile
{
    public sealed record GetProfileQuery():IRequest<List<Profile>>;
}
