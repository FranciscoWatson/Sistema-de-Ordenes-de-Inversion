using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Configurations;

public class TipoActivoConfiguration : IEntityTypeConfiguration<TipoActivo>
{
    public void Configure(EntityTypeBuilder<TipoActivo> builder)
    {
        builder.Property(ta => ta.Descripcion).HasMaxLength(50);
    }
}