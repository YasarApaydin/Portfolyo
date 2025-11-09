using Portfolyo.DataAccess.Context;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.DataAccess.Repositories
{
    internal sealed class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
