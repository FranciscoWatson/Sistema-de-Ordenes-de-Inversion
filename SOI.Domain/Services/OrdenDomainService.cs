namespace SOI.Domain.Services;

public class OrdenDomainService : IOrdenDomainService
{
    public void Validar(int tipoActivoId, decimal? precio)
    {
        if (tipoActivoId == 1 && precio != null)
            throw new ArgumentException("El precio no debe ser ingresado para Acciones.");
        
        if ((tipoActivoId == 2 || tipoActivoId == 3) && precio == null)
            throw new ArgumentException("El precio es obligatorio para Bonos y FCI.");
    }

    public decimal CalcularMontoTotal(int tipoActivoId, decimal precioActivoBD, decimal? precioRequest, decimal cantidad)
    {
        switch (tipoActivoId)
        {
            case 1: // Acción
                var montoAccion = precioActivoBD * cantidad;
                var comisionAcc = montoAccion * 0.006m;
                var impuestosAcc = comisionAcc * 0.21m;
                return montoAccion + comisionAcc + impuestosAcc;

            case 2: // Bono
                var montoBono = (precioRequest ?? 0) * cantidad;
                var comisionBono = montoBono * 0.002m;
                var impuestosBono = comisionBono * 0.21m;
                return montoBono + comisionBono + impuestosBono;

            case 3: // FCI
                var montoFci = (precioRequest ?? 0) * cantidad;
                return montoFci;

            default:
                throw new ArgumentException("Tipo de activo desconocido.");
        }
    }
}