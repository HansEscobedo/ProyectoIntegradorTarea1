using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using libreria.Data;

namespace libreria.pages
{
    public class ListarLibrosModel : PageModel
    {
        public List<Libro> Libros {get;set;}
        public void OnGet()
        {
            ContextoLibreria contexto = new ContextoLibreria();
            Libros = contexto.Libros.ToList();

        }
    }
}
