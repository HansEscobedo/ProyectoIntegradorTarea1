using System.Collections.Generic;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Rut { get; set; }
    public List<Venta> Ventas { get; set; }
}