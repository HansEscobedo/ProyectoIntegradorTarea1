using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace libreria.Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime tiempoActual = DateTime.Now;

            while (true)
            {
                Console.WriteLine("leyendo json de libros");
                DateTime modTime = File.GetLastWriteTime("ingresos.json");
                if (modTime > tiempoActual)
                {
                    Console.WriteLine("entrando al if");
                    tiempoActual = modTime;
                    AgregarLibrosDesdeJson();
                }
                Thread.Sleep(10000);
            }
        }

        public static void AgregarLibrosDesdeJson()
        {
            // Leer el archivo ingresos.json
            string jsonFilePath = "ingresos.json";
            string jsonString = File.ReadAllText(jsonFilePath);

            // Deserializar el JSON a una lista de libros
            List<Libro> libros = JsonSerializer.Deserialize<List<Libro>>(jsonString);

            // Conectar a la base de datos utilizando el contexto
            using (var contexto = new ContextoLibro())
            {
                foreach (var libro in libros)
                {
                    // Buscar si el libro ya existe en la base de datos por su ID
                    var libroExistente = contexto.Libros.FirstOrDefault(l => l.Id == libro.Id);

                    if (libroExistente != null)
                    {
                        // Si el libro ya existe, aumentar el stock
                        libroExistente.Cantidad += libro.Cantidad;
                    }
                    else
                    {
                        // Si el libro no existe, agregarlo a la base de datos
                        contexto.Libros.Add(libro);
                    }
                }

                // Guardar los cambios en la base de datos
                contexto.SaveChanges();
            }

            Console.WriteLine("Libros agregados exitosamente desde el archivo JSON.");
        }
    }
}
