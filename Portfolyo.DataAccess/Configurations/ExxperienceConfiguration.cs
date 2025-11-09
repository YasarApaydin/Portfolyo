using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolyo.Entities.Models;

namespace Portfolyo.DataAccess.Configurations
{
    internal sealed class ExxperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.ToTable("Experience");
            builder.HasKey(x => x.Id);
        }
    }
}
