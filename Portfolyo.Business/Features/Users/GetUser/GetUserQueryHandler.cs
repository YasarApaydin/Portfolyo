using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Features.Users.GetUser
{
    internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<AppUser>>
    {
       private readonly UserManager<AppUser>  userManager;

        public GetUserQueryHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<List<AppUser>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.AsNoTracking().ToListAsync(cancellationToken);

            return user;
        }
    }
}
