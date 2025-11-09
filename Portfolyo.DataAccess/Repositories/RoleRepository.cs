using Portfolyo.DataAccess.Context;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.DataAccess.Repositories
{
    internal sealed class RoleRepository : Repository<AppRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
