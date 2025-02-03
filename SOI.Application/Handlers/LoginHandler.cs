using AutoMapper;
using MediatR;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Domain.Authentication;

namespace SOI.Application.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    
    public LoginHandler(IAuthenticationService authenticationService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }
    
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var tokenInfo = await _authenticationService.LoginAsync(request.Nombre, request.Password);
        var loginResponse = _mapper.Map<LoginResponse>(tokenInfo);
        
        return loginResponse;
    }
    
}