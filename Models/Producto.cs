using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace productoapp.Models
{
    public class Producto
    {
        [Key]
        public int NoProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string nombreProducto { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no puede tener más de 255 caracteres.")]
        public string descripcion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo.")]
        public int Stock { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a 0.")]
        public decimal precioVenta { get; set; }

        [Required(ErrorMessage = "Es necesario seleccionar una categoría.")]
        public int NoCategoria { get; set; }

        // Relación con Categoría
        [ForeignKey("NoCategoria")]
        public Categoria Categoria { get; set; }
    }

}
