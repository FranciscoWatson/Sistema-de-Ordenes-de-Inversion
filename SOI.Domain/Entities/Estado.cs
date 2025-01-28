namespace SOI.Domain.Entities;

public class Estado
{
    public int EstadoId { get; set; }
    public string Descripcion { get; set; }
    
    public List<Orden> Ordenes { get; set; }
}