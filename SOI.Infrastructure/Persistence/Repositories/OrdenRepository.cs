using Microsoft.EntityFrameworkCore;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Repositories;

public class OrdenRepository : IOrdenRepository
{
    private readonly SoiDbContext _context;
    
    public OrdenRepository(SoiDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Orden>> GetAllAsync()
    {
        return await _context.Ordenes.ToListAsync();
    }

    public async Task<Orden?> GetByIdAsync(int id)
    {
        return await _context.Ordenes.FindAsync(id);
    }

    public async Task<Orden> CreateAsync(Orden orden)
    {
        await _context.Ordenes.AddAsync(orden);
        await _context.SaveChangesAsync();
        return orden;
    }

    public async Task UpdateAsync(Orden orden)
    {
        _context.Ordenes.Update(orden);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orden = await _context.Ordenes.FindAsync(id);
        _context.Ordenes.Remove(orden);
        await _context.SaveChangesAsync();
    }
}