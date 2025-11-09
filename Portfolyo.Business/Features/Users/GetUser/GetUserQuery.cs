using MediatR;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Users.GetUser
{
    public sealed record GetUserQuery():IRequest<List<AppUser>>;
}
