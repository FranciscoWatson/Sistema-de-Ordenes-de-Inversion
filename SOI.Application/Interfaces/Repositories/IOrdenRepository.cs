using SOI.Domain.Entities;
namespace SOI.Application.Interfaces.Repositories;

public interface IOrdenRepository
{
    public Task<List<Orden>> GetAllAsync();
    public Task<Orden?> GetByIdAsync(int id);
    public Task<Orden> CreateAsync(Orden orden);
    public Task<Orden> UpdateAsync(Orden orden);
    public Task DeleteAsync(int orden);
    public Task<List<Orden>> GetAllByCuentaAsync(int cuentaId);
}