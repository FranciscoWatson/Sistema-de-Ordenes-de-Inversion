using Microsoft.EntityFrameworkCore;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Repositories;

public class ActivoRepository : IActivoRepository
{
    private readonly SoiDbContext _context;
    
    public ActivoRepository(SoiDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Activo>> GetAllAsync()
    {
        return await _context.Activos.ToListAsync();
    }

    public async Task<Activo?> GetByIdAsync(int id)
    {
        return await _context.Activos
            .Include(a => a.TipoActivo)
            .FirstOrDefaultAsync(a => a.ActivoId == id);
    }

    public async Task<Activo> CreateAsync(Activo activo)
    {
        await _context.Activos.AddAsync(activo);
        await _context.SaveChangesAsync();
        return activo;
    }

    public async Task UpdateAsync(Activo activo)
    {
        _context.Activos.Update(activo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var activo = await _context.Activos.FindAsync(id);
        _context.Activos.Remove(activo);
        await _context.SaveChangesAsync();
    }
}