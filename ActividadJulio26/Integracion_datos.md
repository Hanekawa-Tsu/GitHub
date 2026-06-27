# Integracion de Datos

## Evaluacion Conceptual y Buenas Practicas

### 1. Tabla comparativa de formatos

|         | Ventajas                                                              | Desventajas                                  
| CSV     |Es un formato sencillo, ligero y rapido de procesar se puede           | No permite guardar estructuras complejas ni informacion
|         |abrir facilmente con diferentes programas                              | jerarquica  
| XML     |Organiza los datos de forma estructurada y permite                     | Genera archivos mas grandes y requiere mas tiempo para 
|         |representar relaciones entre ellos                                     | procesarlos.                   

### 2. Diferencia entre Serializacion y Deserializacion

La serializacion consiste en transformar un objeto de C# a un formato como JSON para poder almacenarlo o enviarlo mediante una API. Este proceso normalmente se realiza utilizando JsonSerializer.Serialize()

La deserializacion realiza el proceso inverso. Toma un archivo o una cadena en formato JSON y la convierte nuevamente en un objeto de C#, utilizando JsonSerializer.Deserialize<T>(), para que pueda ser utilizada dentro del programa

### 3. Antipatron N+1

El problema N+1 ocurre cuando durante la lectura de un archivo masivo se realiza una operacion contra la base de datos por cada registro procesado esto provoca una gran cantidad de consultas o inserciones individuales y reduce rendimiento de la aplicacion

La forma recomendada de evitar este problema es almacenar temporalmente todos los registros en una coleccion y, una vez finalizada la lectura del archivo, insertarlos en un solo lote utilizando AddRange() y una unica llamada a SaveChangesAsync()

# Implementacion Practica en C#

## Desafio 1. Consumo de Endpoints y Deserializacion

```csharp
using System.Text.Json;

public class AlumnoService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<Alumno?> ObtenerAlumnoAsync()
    {
        try
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("https://api.usac.edu/v1/alumnos");

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Alumno? alumno = JsonSerializer.Deserialize<Alumno>(json, options);

            return alumno;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}

public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}
```

## Desafio 2. Endpoint para Carga Masiva CSV

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlumnosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("cargar")]
    public async Task<IActionResult> CargarArchivo(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
        {
            return BadRequest("No se recibio ningun archivo.");
        }

        List<Alumno> alumnos = new List<Alumno>();

        using (var reader = new StreamReader(archivo.OpenReadStream()))
        {
            await reader.ReadLineAsync();

            string? linea;

            while ((linea = await reader.ReadLineAsync()) != null)
            {
                string[] datos = linea.Split(',');

                Alumno alumno = new Alumno
                {
                    Nombre = datos[0]
                };

                alumnos.Add(alumno);
            }
        }

        _context.Alumnos.AddRange(alumnos);

        await _context.SaveChangesAsync();

        return Ok("Carga masiva realizada correctamente.");
    }
}

public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}

public class AppDbContext : DbContext
{
    public DbSet<Alumno> Alumnos { get; set; }
}
```

---

# Parte 3. Referencia Bibliografica

Facultad de Ingenieria, USAC. (2026). Sesion 20: Integracion de Datos. Consumo de APIs Externas y Carga Masiva (CSV/XML). Laboratorio del curso Introduccion a la Programacion y Computacion 2. Guatemala.
