using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SOI.Domain.Authentication;
using SOI.Domain.Authentication.Models;
using SOI.Domain.Entities;

namespace SOI.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtAuthenticationConfig _jwtAuthenticationConfig;
    public JwtTokenGenerator(JwtAuthenticationConfig jwtAuthenticationConfig)
    {
        _jwtAuthenticationConfig = jwtAuthenticationConfig;
    }
    public TokenInfo GenerateToken(Cuenta cuenta)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtAuthenticationConfig.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claimsForTokens = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, cuenta.CuentaId.ToString()),
            new Claim(ClaimTypes.GivenName, cuenta.Nombre),
        };
        
        var expiration = DateTime.UtcNow.AddMinutes(_jwtAuthenticationConfig.TokenExpiryInMinutes);
        
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtAuthenticationConfig.Issuer,
            audience: _jwtAuthenticationConfig.Audience,
            claims: claimsForTokens,
            notBefore: DateTime.UtcNow,
            expires: expiration,
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new TokenInfo
        {
            Token = token,
            TokenType = "Bearer",
            ExpiresAt = expiration,
            Cuenta = cuenta
        };
    }
}