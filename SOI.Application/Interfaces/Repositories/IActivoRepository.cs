using SOI.Domain.Entities;

namespace SOI.Application.Interfaces.Repositories;

public interface IActivoRepository
{
    public Task<List<Activo>> GetAllAsync();
    public Task<Activo?> GetByIdAsync(int id);
    public Task<Activo> CreateAsync(Activo activo);
    public Task UpdateAsync(Activo activo);
    public Task DeleteAsync(int id);
}