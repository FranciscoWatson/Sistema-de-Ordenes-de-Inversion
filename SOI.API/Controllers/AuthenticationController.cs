using System.Security.Authentication;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOI.Application.Commands;
using SOI.Application.DTOs;

namespace SOI.API.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequestBody loginRequestBody)
    {
        try
        {
            var command = _mapper.Map<LoginCommand>(loginRequestBody);
            var loginResponse = await _mediator.Send(command);
            
            return Ok(loginResponse);
        }
        catch (AuthenticationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    
}