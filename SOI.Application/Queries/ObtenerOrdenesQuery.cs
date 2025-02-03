using MediatR;
using SOI.Application.DTOs;

namespace SOI.Application.Queries;

public class ObtenerOrdenesQuery : IRequest<List<OrdenResponseDto>>
{
    public int CuentaId { get; set; }
}