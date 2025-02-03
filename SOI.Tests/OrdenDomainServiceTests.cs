using SOI.Domain.Services;

namespace SOI.Tests;

public class OrdenDomainServiceTests
{
    private readonly OrdenDomainService _ordenDomainService;

    public OrdenDomainServiceTests()
    {
        _ordenDomainService = new OrdenDomainService();
    }

    #region Tests para el método Validar

    [Fact]
    public void Validar_AccionConPrecio_DeberiaLanzarArgumentException()
    {
        int tipoActivoId = 1;
        decimal? precio = 100m;

        var exception = Assert.Throws<ArgumentException>(() =>
            _ordenDomainService.Validar(tipoActivoId, precio));

        Assert.Equal("El precio no debe ser ingresado para Acciones.", exception.Message);
    }

    [Fact]
    public void Validar_AccionSinPrecio_NoDeberiaLanzarExcepcion()
    {
        int tipoActivoId = 1;
        decimal? precio = null;

        var exception = Record.Exception(() =>
            _ordenDomainService.Validar(tipoActivoId, precio));

        Assert.Null(exception);
    }

    [Fact]
    public void Validar_BonoSinPrecio_DeberiaLanzarArgumentException()
    {
        int tipoActivoId = 2;
        decimal? precio = null;

        var exception = Assert.Throws<ArgumentException>(() =>
            _ordenDomainService.Validar(tipoActivoId, precio));

        Assert.Equal("El precio es obligatorio para Bonos y FCI.", exception.Message);
    }

    [Fact]
    public void Validar_BonoConPrecio_NoDeberiaLanzarExcepcion()
    {
        int tipoActivoId = 2;
        decimal? precio = 50m;

        var exception = Record.Exception(() =>
            _ordenDomainService.Validar(tipoActivoId, precio));

        Assert.Null(exception);
    }

    [Fact]
    public void Validar_FCISinPrecio_DeberiaLanzarArgumentException()
    {
        int tipoActivoId = 3;
        decimal? precio = null;

        var exception = Assert.Throws<ArgumentException>(() =>
            _ordenDomainService.Validar(tipoActivoId, precio));

        Assert.Equal("El precio es obligatorio para Bonos y FCI.", exception.Message);
    }

    [Fact]
    public void Validar_FCIConPrecio_NoDeberiaLanzarExcepcion()
    {
        int tipoActivoId = 3;
        decimal? precio = 100m;

        var exception = Record.Exception(() =>
            _ordenDomainService.Validar(tipoActivoId, precio));

        Assert.Null(exception);
    }

    #endregion

    #region Tests para el método CalcularMontoTotal

    [Fact]
    public void CalcularMontoTotal_Accion_Caso1()
    {
        int tipoActivoId = 1;
        decimal precioActivoBD = 100;
        decimal? precioRequest = null;
        int cantidad = 10;

        decimal monto = precioActivoBD * cantidad;
        decimal comision = monto * 0.006m;
        decimal impuestos = comision * 0.21m;
        decimal expectedTotal = monto + comision + impuestos;

        var totalCalculado = _ordenDomainService.CalcularMontoTotal(
            tipoActivoId, precioActivoBD, precioRequest, cantidad);

        Assert.Equal(expectedTotal, totalCalculado, 2);
    }

    [Fact]
    public void CalcularMontoTotal_Accion_Caso2()
    {
        int tipoActivoId = 1;
        decimal precioActivoBD = 150;
        decimal? precioRequest = null;
        int cantidad = 20;

        decimal monto = precioActivoBD * cantidad;
        decimal comision = monto * 0.006m;
        decimal impuestos = comision * 0.21m;
        decimal expectedTotal = monto + comision + impuestos;

        var totalCalculado = _ordenDomainService.CalcularMontoTotal(
            tipoActivoId, precioActivoBD, precioRequest, cantidad);

        Assert.Equal(expectedTotal, totalCalculado, 2);
    }

    [Fact]
    public void CalcularMontoTotal_Bono_Caso1()
    {
        int tipoActivoId = 2;
        decimal precioActivoBD = 0m;
        decimal? precioRequest = 50m;
        int cantidad = 10;

        decimal monto = (precioRequest ?? 0) * cantidad;
        decimal comision = monto * 0.002m;
        decimal impuestos = comision * 0.21m;
        decimal expectedTotal = monto + comision + impuestos;

        var totalCalculado = _ordenDomainService.CalcularMontoTotal(
            tipoActivoId, precioActivoBD, precioRequest, cantidad);

        Assert.Equal(expectedTotal, totalCalculado, 2);
    }

    [Fact]
    public void CalcularMontoTotal_Bono_Caso2()
    {
        int tipoActivoId = 2;
        decimal precioActivoBD = 0m;
        decimal? precioRequest = 150m;
        int cantidad = 5;

        decimal monto = (precioRequest ?? 0) * cantidad;
        decimal comision = monto * 0.002m;
        decimal impuestos = comision * 0.21m;
        decimal expectedTotal = monto + comision + impuestos;

        var totalCalculado = _ordenDomainService.CalcularMontoTotal(
            tipoActivoId, precioActivoBD, precioRequest, cantidad);

        Assert.Equal(expectedTotal, totalCalculado, 2);
    }

    [Fact]
    public void CalcularMontoTotal_FCI_Caso1()
    {
        int tipoActivoId = 3;
        decimal precioActivoBD = 0m;
        decimal? precioRequest = 10m;
        int cantidad = 10;

        decimal expectedTotal = (precioRequest ?? 0) * cantidad;

        var totalCalculado = _ordenDomainService.CalcularMontoTotal(
            tipoActivoId, precioActivoBD, precioRequest, cantidad);

        Assert.Equal(expectedTotal, totalCalculado, 2);
    }

    [Fact]
    public void CalcularMontoTotal_FCI_Caso2()
    {
        int tipoActivoId = 3;
        decimal precioActivoBD = 0m;
        decimal? precioRequest = 50m;
        int cantidad = 5;

        decimal expectedTotal = (precioRequest ?? 0) * cantidad;

        var totalCalculado = _ordenDomainService.CalcularMontoTotal(
            tipoActivoId, precioActivoBD, precioRequest, cantidad);

        Assert.Equal(expectedTotal, totalCalculado, 2);
    }

    [Fact]
    public void CalcularMontoTotal_TipoActivoDesconocido_DeberiaLanzarArgumentException()
    {
        int tipoActivoId = 999;
        decimal precioActivoBD = 100;
        decimal? precioRequest = 50;
        int cantidad = 10;

        Assert.Throws<ArgumentException>(() =>
            _ordenDomainService.CalcularMontoTotal(
                tipoActivoId, precioActivoBD, precioRequest, cantidad));
    }

    #endregion
}
