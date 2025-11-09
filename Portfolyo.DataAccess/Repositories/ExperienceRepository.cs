using Portfolyo.DataAccess.Context;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.DataAccess.Repositories
{
    internal sealed class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
