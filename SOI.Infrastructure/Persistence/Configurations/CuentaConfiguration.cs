using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Configurations;

public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
{
    public void Configure(EntityTypeBuilder<Cuenta> builder)
    {
        builder.Property(c => c.Nombre).HasMaxLength(100);
    }
}