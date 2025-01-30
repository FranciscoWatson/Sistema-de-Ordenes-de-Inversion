using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOI.Application.Commands;
using SOI.Application.DTOs;

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
}