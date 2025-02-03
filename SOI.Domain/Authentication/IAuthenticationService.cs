using SOI.Domain.Authentication.Models;

namespace SOI.Domain.Authentication;

public interface IAuthenticationService
{
    Task<TokenInfo> LoginAsync(string nombre, string password);
}