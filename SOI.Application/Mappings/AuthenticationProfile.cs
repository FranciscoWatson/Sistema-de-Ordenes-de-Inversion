using AutoMapper;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Domain.Authentication.Models;

namespace SOI.Application.Mappings;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<LoginRequestBody, LoginCommand>();
        
        CreateMap<TokenInfo, LoginResponse>()
            .ForMember(dest => dest.CuentaResponseDto, opt => opt.MapFrom(src => src.Cuenta));
    }
    
}