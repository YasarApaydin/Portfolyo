using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.DataAccess.Authorization
{
    public sealed class RoleFilterAttribute : TypeFilterAttribute
    {
        public RoleFilterAttribute(string role) : base(typeof(RoleAttribute))
        {
            Arguments = new[] { role };
        }
    }
}
