using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace libreria.Pages
{
    public class RealizarVentaModel : PageModel
    {
        public List<Cliente> Clientes { get; set; }
        public List<Libro> Libros { get; set; }
        public void OnGet()
        {
            using (var contexto = new ContextoLibreria())
            {
                Clientes = contexto.Clientes.Where(c => c.Activo == 1).ToList();
                Libros = contexto.Libros.ToList();
            }
        }

        public async Task<IActionResult> onPostAsync(int IdLibro, int Cantidad, int IdCliente)
        {
            using(var contexto = new ContextoLibreria())
            {
                var libro = await contexto.Libros.FirstOrDefaultAsync(l => l.Id == IdLibro);
                if(libro.Cantidad < Cantidad)
                {
                    TempData["Mensaje"] = "No hay suficiente stock";
                    return RedirectToPage("RealizarVenta");
                }
                Venta venta = new Venta();
                venta.Fecha = System.DateTime.Now;
                venta.Detalles = new List<DetalleVenta>();
                DetalleVenta detalle = new DetalleVenta();
                detalle.IdVenta = venta.Id;
                detalle.IdLibro = IdLibro;
                detalle.Cantidad = Cantidad;
                detalle.libro = libro;
                venta.Detalles.Add(detalle);
                venta.Total = libro.Precio * Cantidad;
                contexto.Ventas.Add(venta);
                Cliente cliente = await contexto.Clientes.FirstOrDefaultAsync(c => c.Id == IdCliente);
                cliente.Ventas.Add(venta);
                libro.Cantidad -= Cantidad;
                await contexto.SaveChangesAsync();
                TempData["Mensaje"] = "Venta realizada con éxito";
                return RedirectToPage("RealizarVenta");

            }
        }
        

    }
}
