using libreria.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace libreria.pages
{
    public class ListarVentasModel : PageModel
    {
        public List<Venta> Ventas { get; set; }
        public void OnGet()
        {
            ContextoLibreria contexto = new ContextoLibreria();
            Ventas = contexto.Ventas.ToList();

        }
    }
}
