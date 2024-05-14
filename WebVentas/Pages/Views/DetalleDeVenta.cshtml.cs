using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using libreria.Data;

namespace libreria.Pages
{
    public class DetalleDeVentaModel : PageModel
    {
        public List<Libro> Libros { get; set; }
        public void OnGet()
        {
            ContextoLibreria contexto = new ContextoLibreria();
            Libros = contexto.Libros.ToList();
        }

        public RedirectToPageResult OnPost(int idVenta, int idLibro, int cantidad)
    {
        using(var contexto = new ContextoLibreria())
        {
            Venta venta = contexto.Ventas.Find(idVenta);
            Libro libro = contexto.Libros.Find(idLibro);
            DetalleVenta detalle = new DetalleVenta();
            detalle.libro = libro;
            detalle.Cantidad = cantidad;
            detalle.venta = venta;
            contexto.DetalleVentas.Add(detalle);
            contexto.SaveChanges();
            return RedirectToPage("/Views/DetalleDeVenta", new { idVenta = idVenta });
        }
    }
    }

    
}
