using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolyo.Entities.Models;

namespace Portfolyo.DataAccess.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {

            builder.HasKey(r => r.Id);

          
            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            builder.ToTable("AspNetRoles");

            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

         
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

          

         

        }
    }
}
