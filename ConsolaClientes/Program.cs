using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using ContextoClientes context = new ContextoClientes();


while(true)
{
    Console.WriteLine("Seleccione una opción:");
    Console.WriteLine("1. Listar clientes");
    Console.WriteLine("2. Crear cliente");
    Console.WriteLine("3. Cambiar estado de cliente");
    Console.WriteLine("4. Generar archivo JSON");
    Console.WriteLine("5. Salir");
    var opcion = Console.ReadLine();

    switch(opcion)
    {
        case "1":
            ListarClientes(context);
            break;
        case "2":
            CrearCliente(context);
            break;
        case "3":
            CambiarEstadoCliente(context);
            break;
        case "4":
            GenerarArchivoJSON(context);
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}

static void ListarClientes(ContextoClientes db)
{
    var clientes =from cliente in db.Clientes select cliente;

    foreach (var cliente in clientes)
        {
             Console.WriteLine($"ID: {cliente.Id}, Nombre: {cliente.Nombre}, Rut: {cliente.Rut}, Activo: {cliente.Activo}");
        }
}

static void CrearCliente(ContextoClientes db)
    {
        Console.WriteLine("Ingrese el nombre del cliente:");
        var nombre = Console.ReadLine();
        Console.WriteLine("Ingrese el Rut del cliente:");
        var rut = Console.ReadLine();
        Console.WriteLine("Ingrese el estado del cliente (1 para activo, 0 para inactivo):");
        var activo = int.Parse(Console.ReadLine());

        var nuevoCliente = new Cliente { Nombre = nombre, Rut = rut, Activo = activo };
        db.Clientes.Add(nuevoCliente);
        db.SaveChanges();
        Console.WriteLine("Nuevo cliente creado.");
    }
static void CambiarEstadoCliente(ContextoClientes db)
    {
        Console.WriteLine("Ingrese el ID del cliente cuyo estado desea cambiar:");
        var clienteId = int.Parse(Console.ReadLine());
        var cliente = db.Clientes.Find(clienteId);
        if (cliente != null)
        {
            Console.WriteLine("Ingrese el nuevo estado del cliente (1 para activo, 0 para inactivo):");
            cliente.Activo = int.Parse(Console.ReadLine());
            db.SaveChanges();
            Console.WriteLine("Estado del cliente cambiado.");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }
  static void GenerarArchivoJSON(ContextoClientes db)
{

    try
    {
        List<Cliente> listaClientes = new List<Cliente>();
        var clientes = from cliente in db.Clientes select cliente;
        foreach (var cliente in clientes)
        {
            listaClientes.Add(cliente);
        }
        string jsonString = JsonSerializer.Serialize(listaClientes,new JsonSerializerOptions { WriteIndented = true });
        string rutaArchivo = @"C:\Users\HansE\OneDrive\Escritorio\Universidad\Cuarto semestre\integrador\Evaluacion1\ActClientes\clientes.json";
        File.WriteAllText(rutaArchivo, jsonString);
         Console.WriteLine($"Archivo JSON modificado exitosamente. Ruta: {Path.GetFullPath(rutaArchivo)}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al generar el archivo JSON: {ex.Message}");
    }
}
