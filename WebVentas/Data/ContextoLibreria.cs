using Microsoft.EntityFrameworkCore;
namespace libreria.Data;
public class ContextoLibreria : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		optionsBuilder.UseMySQL("Server=localhost;Database=libreria;Uid=root;Pwd=xxmlgswegxx22");
	}
	public DbSet<Libro> Libros { get; set; }
	public DbSet<Venta> Ventas { get; set; }
	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<DetalleVenta> DetalleVentas { get; set; }
}
