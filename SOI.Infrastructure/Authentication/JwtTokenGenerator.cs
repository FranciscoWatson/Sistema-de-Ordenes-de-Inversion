using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SOI.Domain.Authentication;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtAuthenticationConfig _jwtAuthenticationConfig;
    public JwtTokenGenerator(JwtAuthenticationConfig jwtAuthenticationConfig)
    {
        _jwtAuthenticationConfig = jwtAuthenticationConfig;
    }
    public string GenerateToken(Cuenta cuenta)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtAuthenticationConfig.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claimsForTokens = new List<Claim>
        {
            new Claim("sub", cuenta.CuentaId.ToString()),
            new Claim("given_name", cuenta.Nombre),
        };
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtAuthenticationConfig.Issuer,
            audience: _jwtAuthenticationConfig.Audience,
            claims: claimsForTokens,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_jwtAuthenticationConfig.TokenExpiryInMinutes),
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }
}