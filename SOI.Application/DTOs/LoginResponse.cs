namespace SOI.Application.DTOs;

public class LoginResponse
{
    public string Token { get; set; }
    public string TokenType { get; set; }
    public DateTime ExpiresAt { get; set; }
    public CuentaResponseDto CuentaResponseDto { get; set; }
}