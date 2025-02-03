using MediatR;
using SOI.Application.DTOs;

namespace SOI.Application.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Nombre { get; set; }
    public string Password { get; set; }
}