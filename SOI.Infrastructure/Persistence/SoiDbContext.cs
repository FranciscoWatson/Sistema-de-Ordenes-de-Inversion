using Microsoft.EntityFrameworkCore;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence;

public class SoiDbContext : DbContext
{
    public SoiDbContext(DbContextOptions<SoiDbContext> options) : base(options)
    {
    }
    
    public DbSet<Activo> Activos { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Orden> Ordenes { get; set; }
    public DbSet<TipoActivo> TiposActivos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SoiDbContext).Assembly);
    }
}