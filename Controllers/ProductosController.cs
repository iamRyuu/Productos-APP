using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using productoapp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace productoapp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly InventarioContext _context;

        public ProductosController(InventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Producto.Include(p => p.Categoria).ToListAsync();
            ViewData["NoCategoria"] = new SelectList(_context.Categoria, "NoCategoria", "nombreCategoria");
            return View(productos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductoAjax([Bind("nombreProducto, descripcion, Stock, precioVenta, NoCategoria")] Producto producto)
        {
            try
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Producto creado con éxito" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al crear el producto", error = ex.Message });
            }


            return Json(new
            {
                success = false,
                message = "Error al crear el producto",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "Producto no encontrado" });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    noProducto = producto.NoProducto,
                    nombreProducto = producto.nombreProducto,
                    descripcion = producto.descripcion,
                    stock = producto.Stock,
                    precioVenta = producto.precioVenta,
                    noCategoria = producto.NoCategoria
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditProductoAjax([Bind("NoProducto, nombreProducto, descripcion, Stock, precioVenta, NoCategoria")] Producto producto)
        {
            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Producto modificado con éxito" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al modificar el producto", error = ex.Message });
            }

            return Json(new
            {
                success = false,
                message = "Error al modificar el producto",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductoAjax(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "Producto no encontrado" });
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Producto eliminado con éxito" });
        }
    }
}
