using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOI.Application.Commands;
using SOI.Application.DTOs;
using SOI.Application.Queries;

namespace SOI.API.Controllers;

[ApiController]
[Route("api/ordenes")]
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
        var result = await _mediator.Send(new ObtenerOrdenesQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerOrdenPorId(int id)
    {
        var result = await _mediator.Send(new ObtenerOrdenPorIdQuery { CuentaId = id });
        return Ok(result);
    }
    
    // To-DO: Validar dependiendo el tipo de activo que tiene que enviar.
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarOrden(int id, [FromBody] ActualizarOrdenDto request)
    {
        var command = _mapper.Map<ActualizarOrdenCommand>(request);
        command.OrdenId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}