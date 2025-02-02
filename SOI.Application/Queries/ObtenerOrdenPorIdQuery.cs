using MediatR;
using SOI.Application.DTOs;

namespace SOI.Application.Queries;

public class ObtenerOrdenPorIdQuery : IRequest<OrdenResponseDto>
{
    public int CuentaId { get; set; }
}