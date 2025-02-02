using MediatR;
using SOI.Application.DTOs;

namespace SOI.Application.Commands;

public class ActualizarOrdenCommand : IRequest<OrdenResponseDto>
{
    public int OrdenId { get; set; }
    public int CuentaId { get; set; }   
    public int ActivoId { get; set; }
    public int Cantidad { get; set; }
    public decimal? Precio { get; set; }  
    public char Operacion { get; set; }
}