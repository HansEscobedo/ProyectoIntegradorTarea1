using Microsoft.EntityFrameworkCore;

public class ContextoClientes : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySQL("server=localhost;database=Clientes;user=root;password=xxmlgswegxx22;");

    }

    public DbSet<Cliente> Clientes {get; set;}

    
}