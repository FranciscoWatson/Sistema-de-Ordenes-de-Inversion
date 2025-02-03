using Microsoft.EntityFrameworkCore;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Persistence.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly SoiDbContext _context;
    
    public CuentaRepository(SoiDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Cuenta>> GetAllAsync()
    {
        return await _context.Cuentas.ToListAsync();
    }

    public async Task<Cuenta?> GetByIdAsync(int id)
    {
        return await _context.Cuentas.FindAsync(id);
    }

    public async Task<Cuenta> CreateAsync(Cuenta cuenta)
    {
        await _context.Cuentas.AddAsync(cuenta);
        await _context.SaveChangesAsync();
        return cuenta;
    }

    public async Task UpdateAsync(Cuenta cuenta)
    {
        _context.Cuentas.Update(cuenta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cuenta = await _context.Cuentas.FindAsync(id);
        _context.Cuentas.Remove(cuenta);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Cuenta?> AuthenticateAsync(string nombre, string password)
    {
        return await _context.Cuentas
            .FirstOrDefaultAsync(c => c.Nombre == nombre && c.Password == password);
    }
}