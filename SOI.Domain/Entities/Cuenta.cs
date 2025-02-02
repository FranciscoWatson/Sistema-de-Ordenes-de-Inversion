namespace SOI.Domain.Entities;

public class Cuenta
{
    public int CuentaId { get; set; }
    public string Nombre { get; set; }
    public string HashedPassword { get; set; }

    public List<Orden> Ordenes { get; set; }
}