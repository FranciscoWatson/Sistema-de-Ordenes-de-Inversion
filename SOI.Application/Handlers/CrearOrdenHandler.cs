using AutoMapper;
using MediatR;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Application.Interfaces.Repositories;

namespace SOI.Application.Handlers;

public class CrearOrdenHandler : IRequestHandler<CrearOrdenCommand, OrdenResponseDto>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IActivoRepository _activoRepository;
    private readonly IMapper _mapper;
    
    public CrearOrdenHandler(IOrdenRepository ordenRepository, IActivoRepository activoRepository, IMapper mapper)
    {
        _ordenRepository = ordenRepository;
        _activoRepository = activoRepository;
        _mapper = mapper;
    }
    
    public Task<OrdenResponseDto> Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}