using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Application.Queries;

namespace SOI.API.Controllers;

[ApiController]
[Route("api/ordenes")]
[Authorize]
public class OrdenController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public OrdenController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CrearOrden([FromBody] CrearOrdenDto request)
    {
        var command = _mapper.Map<CrearOrdenCommand>(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerOrdenes()
    {
        var cuentaIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(cuentaIdClaim) || !int.TryParse(cuentaIdClaim, out int cuentaId))
            return Unauthorized("Usuario no autenticado o ID inválido.");
        
        var result = await _mediator.Send(new ObtenerOrdenesQuery { CuentaId = cuentaId });
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerOrdenPorId(int id)
    {
        var cuentaIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(cuentaIdClaim) || !int.TryParse(cuentaIdClaim, out int cuentaId))
            return Unauthorized("Usuario no autenticado o ID inválido.");
        var result = await _mediator.Send(new ObtenerOrdenPorIdQuery { OrderId = id, CuentaId = cuentaId });
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarOrden(int id, [FromBody] ActualizarOrdenDto request)
    {
        var cuentaIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(cuentaIdClaim) || !int.TryParse(cuentaIdClaim, out int cuentaId))
            return Unauthorized("Usuario no autenticado o ID inválido.");
        
        if (cuentaId != request.CuentaId)
            return Unauthorized("No tienes permiso para actualizar esta orden.");
        
        var command = _mapper.Map<ActualizarOrdenCommand>(request);
        command.OrdenId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarOrden(int id)
    {
        var cuentaIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(cuentaIdClaim) || !int.TryParse(cuentaIdClaim, out int cuentaId))
            return Unauthorized("Usuario no autenticado o ID inválido.");
        
        await _mediator.Send(new EliminarOrderCommand { OrdenId = id, CuentaId = cuentaId });
        return Ok();
    }
}