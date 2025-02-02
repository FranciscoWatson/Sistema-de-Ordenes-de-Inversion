using AutoMapper;
using FluentValidation;
using MediatR;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Application.Interfaces.Repositories;
using SOI.Domain.Services;

namespace SOI.Application.Handlers;

public class ActualizarOrdenHandler : IRequestHandler<ActualizarOrdenCommand, OrdenResponseDto>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IActivoRepository _activoRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMapper _mapper;
    private readonly IOrdenDomainService _ordenDomainService;
    private readonly IValidator<ActualizarOrdenCommand> _validator;
    
    public ActualizarOrdenHandler(IOrdenRepository ordenRepository, IActivoRepository activoRepository, ICuentaRepository cuentaRepository, IMapper mapper, IOrdenDomainService ordenDomainService, IValidator<ActualizarOrdenCommand> validator)
    {
        _ordenRepository = ordenRepository;
        _activoRepository = activoRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
        _ordenDomainService = ordenDomainService;
        _validator = validator;
    }
    
    public async Task<OrdenResponseDto> Handle(ActualizarOrdenCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var orden = await _ordenRepository.GetByIdAsync(request.OrdenId);
        if (orden == null)
            throw new Exception("La orden no existe.");

        var activo = await _activoRepository.GetByIdAsync(request.ActivoId);
        if (activo == null)
            throw new Exception("Activo no encontrado.");

        var cuenta = await _cuentaRepository.GetByIdAsync(request.CuentaId);
        if (cuenta == null)
            throw new Exception("Cuenta no encontrada.");

        _ordenDomainService.Validar(activo.TipoActivoId, request.Precio);

        var montoTotal = _ordenDomainService.CalcularMontoTotal(
            activo.TipoActivoId,
            activo.PrecioUnitario,
            request.Precio,
            request.Cantidad
        );
        
        orden.CuentaId = request.CuentaId;
        orden.ActivoId = request.ActivoId;
        orden.Cantidad = request.Cantidad;
        orden.Precio = request.Precio;
        orden.Operacion = request.Operacion;
        orden.MontoTotal = montoTotal;

        var result = await _ordenRepository.UpdateAsync(orden);
        return _mapper.Map<OrdenResponseDto>(result);
    }
}