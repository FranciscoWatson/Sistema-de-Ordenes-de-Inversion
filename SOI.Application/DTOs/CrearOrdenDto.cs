﻿namespace SOI.Application.DTOs;

public class CrearOrdenDto
{
    public int CuentaId { get; set; }   
    public int ActivoId { get; set; }
    public int Cantidad { get; set; }
    public decimal? Precio { get; set; }  
    public char Operacion { get; set; }
}