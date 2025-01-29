using AutoMapper;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Domain.Entities;

namespace SOI.Application.Mappings;

public class OrdenProfile : Profile
{
    public OrdenProfile()
    {
        CreateMap<Orden, OrdenResponseDto>().ReverseMap();
        CreateMap<CrearOrdenDto, CrearOrdenCommand>().ReverseMap();
        
    }
    
}