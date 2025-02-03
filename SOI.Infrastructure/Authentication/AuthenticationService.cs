using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Authentication;
using SOI.Domain.Authentication.Models;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICuentaRepository _cuentaRepository;
    
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, ICuentaRepository cuentaRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _cuentaRepository = cuentaRepository;
    }
    
    public async Task<TokenInfo> LoginAsync(string nombre, string password)
    {
        var cuenta = await ValidarCredencialesCuenta(nombre, password);
        if (cuenta == null)
        {
            throw new AuthenticationException("Username or password is incorrect.");
        }
        
        var tokenInfo = _jwtTokenGenerator.GenerateToken(cuenta);
        
        return tokenInfo;
    }
    
    private async Task<Cuenta?> ValidarCredencialesCuenta(string nombre, string password)
    {
        var cuenta = await _cuentaRepository.AuthenticateAsync(nombre, password); //add HashPassword method for password here, removed it for testing purposes
        return cuenta;
    }
    
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
    }
}