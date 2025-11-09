using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Portfolyo.Entities.Repositories;
using System.Security.Claims;

namespace Portfolyo.DataAccess.Authorization
{
    public sealed class RoleAttribute : Attribute, IAuthorizationFilter
    {

        private readonly string role;
        private readonly IUserRoleRepository userRoleRepository;

        public RoleAttribute(IUserRoleRepository userRoleRepository, string role)
        {
            this.userRoleRepository = userRoleRepository;
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userHasRole = userRoleRepository.GetWhere(p => p.AppUserId.ToString() == userIdClaim.Value).Include(p => p.AppRole).Any(p => p.AppRole.Name == role);


            if (!userHasRole)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


        }





    }
}
