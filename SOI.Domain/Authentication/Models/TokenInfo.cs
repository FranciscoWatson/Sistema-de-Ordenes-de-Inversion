using SOI.Domain.Entities;

namespace SOI.Domain.Authentication.Models;

public class TokenInfo
{
    public string Token { get; set; }
    public string TokenType { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Cuenta Cuenta { get; set; }
}