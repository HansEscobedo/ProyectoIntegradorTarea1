public class DetalleVenta
{
    public int Id { get; set; }
    public int IdVenta { get; set; }
    public Venta venta { get; set; }
    public int IdLibro { get; set; }
    public Libro libro { get; set; }
    public int Cantidad { get; set; }
}