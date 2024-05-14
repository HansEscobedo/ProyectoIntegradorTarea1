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
        public void OnGet()
        {
            using (var contexto = new ContextoLibreria())
            {
                Clientes = contexto.Clientes.Where(c => c.Activo == 1).ToList();
                
            }
        }

        public RedirectToPageResult OnPost(int cliente)
        {
            using (var contexto = new ContextoLibreria())
            {
                Cliente c = contexto.Clientes.Include(x=>x.Ventas).SingleOrDefault(x => x.Id == cliente);
                Venta v = new Venta();
                v.Fecha = System.DateTime.Now;
                v.Total = 0;
                contexto.Ventas.Add(v);
                contexto.SaveChanges();
                c.Ventas.Add(v);
                contexto.SaveChanges();
                return RedirectToPage("/Views/DetalleDeVenta", new { idVenta = v.Id });
            }
            
        }
        

    }
}
