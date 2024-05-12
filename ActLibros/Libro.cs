using System.Collections.Generic;

public class Libro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public int Cantidad { get; set; }
    public int Precio { get; set; }
    public List<Venta> Ventas { get; set; }
}
