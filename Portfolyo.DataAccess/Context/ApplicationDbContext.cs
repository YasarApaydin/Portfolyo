using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Repositories;
using System.Reflection.Emit;

namespace Portfolyo.DataAccess.Context
{
    internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>,IUnitOfWork
    {
      
            
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

          
        }

    }
}
