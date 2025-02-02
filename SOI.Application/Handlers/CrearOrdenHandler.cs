using AutoMapper;
using MediatR;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Entities;
using SOI.Domain.Services;

namespace SOI.Application.Handlers;

public class CrearOrdenHandler : IRequestHandler<CrearOrdenCommand, OrdenResponseDto>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IActivoRepository _activoRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMapper _mapper;
    private readonly IOrdenDomainService _ordenDomainService;
    
    public CrearOrdenHandler(IOrdenRepository ordenRepository, IActivoRepository activoRepository, ICuentaRepository cuentaRepository, IMapper mapper, IOrdenDomainService ordenDomainService)
    {
        _ordenRepository = ordenRepository;
        _activoRepository = activoRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
        _ordenDomainService = ordenDomainService;
    }
    
    public async Task<OrdenResponseDto> Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
    {
        var activo = await _activoRepository.GetByIdAsync(request.ActivoId);
        if (activo == null)
        {
            throw new Exception("Activo no encontrado");
        }
        
        var cuenta = await _cuentaRepository.GetByIdAsync(request.CuentaId);
        if (cuenta == null)
        {
            throw new Exception("Cuenta no encontrada");
        }
        
        _ordenDomainService.Validar(activo.TipoActivoId, request.Precio);

        var montoTotal = _ordenDomainService.CalcularMontoTotal(
            activo.TipoActivoId,
            activo.PrecioUnitario,
            request.Precio,
            request.Cantidad
        );
        
        var orden = new Orden
        {
            CuentaId = request.CuentaId,
            ActivoId = request.ActivoId,
            Cantidad = request.Cantidad,
            Precio = request.Precio,
            Operacion = request.Operacion,
            MontoTotal = montoTotal
        };
        
        var result = await _ordenRepository.CreateAsync(orden);
        
        return _mapper.Map<OrdenResponseDto>(result);
    }
}