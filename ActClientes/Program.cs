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
                Console.WriteLine("leyendo json de clientes");
                DateTime modTime = File.GetLastWriteTime("clientes.json");
                if (modTime > tiempoActual)
                {
                    Console.WriteLine("entrando al if");
                    tiempoActual = modTime;
                    AgregarClientesDesdeJson();
                }
                Thread.Sleep(10000);
            }
        }

        public static void AgregarClientesDesdeJson()
        {

            String jsonFilePath = "clientes.json";
            String jsonString = File.ReadAllText(jsonFilePath);
            List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>(jsonString);
            using (var db = new ContextoCliente())
            {
                foreach (var cliente in clientes)
                {
                    var clienteEncontrado = db.Clientes.FirstOrDefault(c => c.Rut == cliente.Rut);
                    if (clienteEncontrado == null)
                    {
                        db.Clientes.Add(cliente);
                    }
                    else
                    {
                        clienteEncontrado.Activo = cliente.Activo;
                    }
                }
                db.SaveChanges();
            }
            Console.WriteLine("Clientes agregados desde el archivo JSON");
        }
    }
}