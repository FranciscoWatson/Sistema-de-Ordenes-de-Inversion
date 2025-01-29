using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Configurations;

public class ActivoConfiguration : IEntityTypeConfiguration<Activo>
{
    public void Configure(EntityTypeBuilder<Activo> builder)
    {
        builder.HasKey(a => a.ActivoId);
        builder.Property(a => a.Nombre).HasMaxLength(100);
        builder.Property(a => a.Ticker).HasMaxLength(32);
        builder.Property(a => a.PrecioUnitario).HasColumnType("decimal(18,4)");
    }
    
}