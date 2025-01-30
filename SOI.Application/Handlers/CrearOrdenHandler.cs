using AutoMapper;
using MediatR;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Entities;

namespace SOI.Application.Handlers;

public class CrearOrdenHandler : IRequestHandler<CrearOrdenCommand, OrdenResponseDto>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IActivoRepository _activoRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMapper _mapper;
    
    public CrearOrdenHandler(IOrdenRepository ordenRepository, IActivoRepository activoRepository, ICuentaRepository cuentaRepository, IMapper mapper)
    {
        _ordenRepository = ordenRepository;
        _activoRepository = activoRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
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
        
        if (activo.TipoActivoId == 1 && request.Precio != null)
            throw new ArgumentException("El precio no debe ser ingresado para Acciones.");

        if ((activo.TipoActivoId == 2 || activo.TipoActivoId == 3) && request.Precio == null)
            throw new ArgumentException("El precio es obligatorio para Bonos y FCI.");
        
        decimal montoTotal = CalcularMontoTotal(request, activo);
        
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

    private decimal CalcularMontoTotal(CrearOrdenCommand request, Activo activo)
    {
        decimal montoTotal = 0;
        switch (activo.TipoActivoId)
        {
            case 1: // Acción
                montoTotal = activo.PrecioUnitario * request.Cantidad;
                decimal comision = montoTotal * 0.006m;
                decimal impuestos = comision * 0.21m;
                return montoTotal + comision + impuestos;

            case 2: // Bono
                montoTotal = (decimal)(request.Precio * request.Cantidad)!;
                comision = montoTotal * 0.002m;
                impuestos = comision * 0.21m;
                return montoTotal + comision + impuestos;

            case 3: // FCI
                return (decimal)(request.Precio * request.Cantidad)!;

            default:
                throw new ArgumentException("Tipo de activo desconocido.");
        }
    }
}