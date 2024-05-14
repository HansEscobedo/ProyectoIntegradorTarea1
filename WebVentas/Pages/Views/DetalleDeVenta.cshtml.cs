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
        public int idVenta { get; set; }
        public void OnGet(int idVenta)
        {
            ContextoLibreria contexto = new ContextoLibreria();
            Libros = contexto.Libros.ToList();
            this.idVenta = idVenta;
        }

        public RedirectToPageResult OnPost(int idVenta, int idLibro, int cantidad)
    {
        using(var contexto = new ContextoLibreria())
        {
            Venta venta = contexto.Ventas.Find(idVenta);
            Libro libro = contexto.Libros.Find(idLibro);
            DetalleVenta detalle = new DetalleVenta();
            venta.Total += libro.Precio * cantidad;
            libro.Cantidad -= cantidad;
            detalle.libro = libro;
            detalle.Cantidad = cantidad;
            detalle.venta = venta;
            detalle.IdLibro = libro.Id;
            detalle.IdVenta = venta.Id;
            contexto.DetalleVentas.Add(detalle);
            contexto.Ventas.Update(venta);
            contexto.SaveChanges();
            return RedirectToPage("/Views/DetalleDeVenta", new { idVenta = idVenta });
        }
    }
    }

    
}
