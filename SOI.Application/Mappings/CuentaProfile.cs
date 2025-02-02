using AutoMapper;
using SOI.Application.DTOs;
using SOI.Domain.Entities;

namespace SOI.Application.Mappings;

public class CuentaProfile : Profile
{
    public CuentaProfile()
    {
        CreateMap<Cuenta, CuentaResponseDto>().ReverseMap();
    }
}