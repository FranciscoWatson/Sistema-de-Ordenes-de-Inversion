namespace SOI.Domain.Services;

public interface IOrdenDomainService
{
    public void Validar(int tipoActivoId, decimal? precio);
    public decimal CalcularMontoTotal(int tipoActivoId, decimal precioActivoBD, decimal? precioRequest, decimal cantidad);
}