using MediatR;

namespace SOI.Application.Commands;

public class EliminarOrderCommand : IRequest
{
    public int OrdenId { get; set; }
    public int CuentaId { get; set; }
}