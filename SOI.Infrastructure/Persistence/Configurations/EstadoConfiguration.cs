using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Configurations;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.Property(e => e.Descripcion).HasMaxLength(100);
    }
}