using SOI.Domain.Authentication.Models;
using SOI.Domain.Entities;

namespace SOI.Domain.Authentication;

public interface IJwtTokenGenerator
{
    TokenInfo GenerateToken(Cuenta cuenta);
}