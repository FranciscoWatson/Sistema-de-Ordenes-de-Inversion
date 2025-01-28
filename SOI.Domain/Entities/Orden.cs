namespace SOI.Domain.Entities;

public class Orden
{
    public int OrdenId { get; set; }
    public int CuentaId { get; set; }
    public int ActivoId { get; set; }
    public int EstadoId { get; set; }
    public char Operacion { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal MontoTotal { get; set; }
    
    public Cuenta Cuenta { get; set; }
    public Activo Activo { get; set; }
    public Estado Estado { get; set; }
}