using SOI.Domain.Entities;

namespace SOI.Application.Interfaces.Repositories;

public interface ICuentaRepository
{
    public Task<List<Cuenta>> GetAllAsync();
    public Task<Cuenta?> GetByIdAsync(int id);
    public Task<Cuenta> CreateAsync(Cuenta cuenta);
    public Task UpdateAsync(Cuenta cuenta);
    public Task DeleteAsync(int cuenta);
    Task<Cuenta?> AuthenticateAsync(string nombre, string password);
}