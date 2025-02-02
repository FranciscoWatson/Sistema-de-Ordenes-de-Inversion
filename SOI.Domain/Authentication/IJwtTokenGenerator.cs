using SOI.Domain.Entities;

namespace SOI.Domain.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Cuenta cuenta);
}