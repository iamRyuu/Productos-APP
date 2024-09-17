using productoapp.Models;
using System.ComponentModel.DataAnnotations;

public class Categoria
{
    [Key]
    public int NoCategoria { get; set; }
    public string nombreCategoria { get; set; }
    public string descripcion { get; set; }

    // Relación con Productos
    public ICollection<Producto> Productos { get; set; }
}
