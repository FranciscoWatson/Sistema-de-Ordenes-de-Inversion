namespace SOI.Application.DTOs;

public class OrdenResponseDto
{
    public int OrdenId { get; set; }
    public int CuentaId { get; set; }
    public int ActivoId { get; set; }
    public int EstadoId { get; set; }
    public char Operacion { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal MontoTotal { get; set; }
}