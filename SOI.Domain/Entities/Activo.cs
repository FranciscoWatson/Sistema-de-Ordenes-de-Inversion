namespace SOI.Domain.Entities;

public class Activo
{
    public int ActivoId { get; set; }
    public string Nombre { get; set; }
    public string Ticker { get; set; }
    public int TipoActivoId { get; set; }
    public decimal PrecioUnitario { get; set; }
    
    public TipoActivo TipoActivo { get; set; }
}