namespace SOI.Domain.Entities;

public class TipoActivo
{
    public int TipoActivoId { get; set; }
    public string Descripcion { get; set; }
    
    public List<Activo> Activos { get; set; }
}