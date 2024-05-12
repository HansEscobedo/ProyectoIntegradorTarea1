using System;
using System.Collections.Generic;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public List<DetalleVenta> Detalles { get; set; }
    public int Total { get; set; }
}