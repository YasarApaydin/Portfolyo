using Portfolyo.DataAccess.Context;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;

namespace Portfolyo.DataAccess.Repositories
{
    internal sealed class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
