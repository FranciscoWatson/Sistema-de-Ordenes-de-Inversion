using AutoMapper;
using MediatR;
using SOI.Application.DTOs;
using SOI.Application.Interfaces.Repositories;
using SOI.Application.Queries;

namespace SOI.Application.Handlers;

public class ObtenerOrdenPorIdHandler : IRequestHandler<ObtenerOrdenPorIdQuery, OrdenResponseDto>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IMapper _mapper;
    
    public ObtenerOrdenPorIdHandler(IOrdenRepository ordenRepository, IMapper mapper)
    {
        _ordenRepository = ordenRepository;
        _mapper = mapper;
    }
    
    public async Task<OrdenResponseDto> Handle(ObtenerOrdenPorIdQuery request, CancellationToken cancellationToken)
    {
        var orden = await _ordenRepository.GetByIdAsync(request.CuentaId);
        return _mapper.Map<OrdenResponseDto>(orden);
    }
}