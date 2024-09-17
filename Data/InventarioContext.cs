using Microsoft.EntityFrameworkCore;
using productoapp.Models; // Asegúrate de que la ruta del namespace sea la correcta.

public class InventarioContext : DbContext
{
    public InventarioContext(DbContextOptions<InventarioContext> options) : base(options)
    {
    }

    public DbSet<Producto> Producto { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
}
