using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Configurations;

public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
{
    public void Configure(EntityTypeBuilder<Orden> builder)
    {
        builder.Property(o => o.Precio).HasColumnType("decimal(18,2)");
        builder.Property(o => o.MontoTotal).HasColumnType("decimal(18,4)");
    }
}